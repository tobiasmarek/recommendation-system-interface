using System;
using System.Collections.Generic;
using System.IO;

namespace RecommendationSystemInterface.Interfaces
{
    internal interface IPreProcessor
    {
        float[][] Preprocess(IDisposableLineReader rr);
    }




    class TfIdf : IPreProcessor
    {
        private Dictionary<string, long> _rowAppearance { get; set; } // In how many rows has the word appeared
        private Dictionary<string, bool> _uniqueWords { get; set; }  // How many unique words are there

        private readonly Dictionary<string, int> _wordIndexMap = new();
        private long numOfRows { get; set; } = 0;

        public float[][] Preprocess(IDisposableLineReader rr) // vubec nemusi byt 2D  // pouzit Math.Numerics kde jsou efficient matice
        {
            _rowAppearance = new();
            _uniqueWords = new();

            string tempTfFilePath = Path.GetTempFileName();

            GetFrequencies(rr, tempTfFilePath);
            string tfIdfFilePath = GetTfIdfFile(numOfRows, tempTfFilePath);

            File.Delete(tempTfFilePath);

            return VectorizeData(numOfRows, _uniqueWords.Count, tfIdfFilePath);
        }

        private float[][] VectorizeData(long height, int width, string tfIdfFilePath) // nevyndat tohle do IVectorizer?
        {
            var sr = new FileStreamWordReader(tfIdfFilePath, new char[] { ',', '\n', '\r' });

            var matrix = new float[height][];
            matrix[0] = new float[width];

            string? wordPair;
            int i = 0;
            while ((wordPair = sr.ReadWord()) != null)
            {
                string[] splitPair = wordPair.Split(' ');

                string word = splitPair[0];
                float number = float.Parse(splitPair[1]);

                if (!_wordIndexMap.ContainsKey(word)) { _wordIndexMap[word] = _wordIndexMap.Count; }

                matrix[i][_wordIndexMap[word]] = number;

                if (sr.EndOfLine && i != height - 1) { i++; matrix[i] = new float[width]; }
            }

            return matrix;
        }

        private string GetTfIdfFile(long numOfRows, string tempTfFilePath)
        {
            string tfIdfFilePath = "tf_idf.txt";

            var sr = new FileStreamWordReader(tempTfFilePath, new char[] { ',', '\n', '\r', });
            var sw = new FileStreamWriter(tfIdfFilePath, false);

            string? wordPair;
            while ((wordPair = sr.ReadWord()) != null)
            {
                string[] splitPair = wordPair.Split(' ');

                string word = splitPair[0];
                float number = float.Parse(splitPair[1]);

                sw.Write($"{word} {number * (float)Math.Log2(numOfRows / (double)_rowAppearance[word])}"); // base?
                // někdy se float při ToString dá do tvaru 10E-7 or smth ... to prekazit
                if (sr.EndOfLine != true) { sw.Write(","); }
                else { sw.Write("\n"); }
            }

            sw.Dispose();
            sr.Dispose();

            return tfIdfFilePath;
        }

        private void GetFrequencies(IDisposableLineReader rr, string tempTfFilePath)
        {
            var sr = new RecordStreamWordReader(rr, new[]
            {
                ' ', '\t', '\n', '\r', // Whitespace characters
                '!', '"', '#', '$', '%', '&', '\'', '(', ')', // Punctuation marks
                '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{',
                '|', '}', '~' // Symbols
            }); // tohle cely by měla být class nebo interface, kterej muzu definovat zvenku
            
            var sw = new FileStreamWriter(tempTfFilePath, true);

            var rowUniqueWordsCount = new Dictionary<string, int>();

            string? word = "";
            while (word is not null)
            {
                int rowWordCount = 0;
                numOfRows++;

                while ((word = sr.ReadWord()) != null)
                {
                    word = word.ToLower(); // abych předešel zmatkům

                    if (!rowUniqueWordsCount.ContainsKey(word))
                    {
                        rowUniqueWordsCount[word] = 0;
                    }

                    if (rowUniqueWordsCount[word] == 0)
                    {
                        if (!_rowAppearance.ContainsKey(word))
                        {
                            _rowAppearance[word] = 0;
                        }

                        _rowAppearance[word]++;
                    }

                    _uniqueWords[word] = true;

                    rowUniqueWordsCount[word]++;
                    rowWordCount++;

                    if (sr.EndOfRow) { break; }
                }

                int keyCounter = 1;
                int numberOfKeys = rowUniqueWordsCount.Keys.Count;
                foreach (var uniqueWord in rowUniqueWordsCount.Keys)
                {
                    sw.Write($"{uniqueWord} {rowUniqueWordsCount[uniqueWord] / (float)rowWordCount}");

                    if (keyCounter != numberOfKeys) { sw.Write(","); }
                    else { sw.Write("\n"); }

                    keyCounter++;
                }

                rowUniqueWordsCount.Clear();
            }

            sw.Dispose();
        }
    }


    class UserItemMatrixPreProcessor : IPreProcessor
    {
        public float[][] Preprocess(IDisposableLineReader rr)
        {
            Dictionary<int, Dictionary<int, int>> userRatings = new();
            var sr = new RecordStreamWordReader(rr, new char[] { '\t' }); // lepsi je rowreader v tomhle pripade

            int userMaxIndex = 0;
            int itemMaxIndex = 0;

            string? word = "";
            while (word is not null) // zase tohle by měl být spis nejakej celej Reader kterej dává vždy jeden záznam ve stringu odkudkoliv
            {
                bool failedToParse = false;
                int k = 0;
                int[] userItemRating = new int[3]; 

                while ((word = sr.ReadWord()) != null)
                {
                    if (sr.EndOfRow) { break; }

                    if (!int.TryParse(word, out userItemRating[k])) { break; }

                    k++;
                }

                if (failedToParse == false)
                {
                    if (!userRatings.ContainsKey(userItemRating[0]))
                    {
                        userRatings[userItemRating[0]] = new Dictionary<int, int>();
                    }

                    if (userItemRating[0] > userMaxIndex) { userMaxIndex = userItemRating[0]; }
                    if (userItemRating[1] > itemMaxIndex) { itemMaxIndex = userItemRating[1]; }

                    userRatings[userItemRating[0]].Add(userItemRating[1], userItemRating[2]);
                }
            }

            float[][] userItemMatrix = new float[userMaxIndex + 1][];

            foreach (var userIndex in userRatings.Keys)
            {
                userItemMatrix[userIndex] = new float[itemMaxIndex + 1];

                foreach (var keyValPair in userRatings[userIndex])
                {
                    userItemMatrix[userIndex][keyValPair.Key] = keyValPair.Value;
                }
            }

            return userItemMatrix;
        }
    }
}
