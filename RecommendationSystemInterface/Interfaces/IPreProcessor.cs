using System;
using System.Collections.Generic;
using System.IO;

namespace RecommendationSystemInterface.Interfaces
{
    /// <summary>
    /// The first processing of incoming data.
    /// Converts or transforms the input into its processed vector form.
    /// </summary>
    public interface IPreProcessor
    {
        float[][] Preprocess(IDisposableLineReader rr);
    }




    /// <summary>
    /// Converts words of each record to its Term Frequency–Inverse Document Frequency for the evaluation
    /// of relevancy and for the sake of vectorization.
    /// </summary>
    class TfIdf : IPreProcessor
    {
        private Dictionary<string, long> _rowAppearance { get; set; } // In how many rows has the word appeared
        private Dictionary<string, bool> _uniqueWords { get; set; }  // How many unique words are there

        private readonly Dictionary<string, int> _wordIndexMap = new(); // For fast and stable indexation of present words
        private long numOfRows { get; set; } = 0;

        public float[][] Preprocess(IDisposableLineReader rr) // NEMUSI BYT 2D  // POUZIT Math.Numerics EFFICIENT MATICE ?
        {
            _rowAppearance = new();
            _uniqueWords = new();

            string tempTfFilePath = Path.GetTempFileName();

            GetFrequencies(rr, tempTfFilePath);
            string tfIdfFilePath = GetTfIdfFile(tempTfFilePath);

            File.Delete(tempTfFilePath);

            return VectorizeData(numOfRows, _uniqueWords.Count, tfIdfFilePath);
        }

        /// <summary>
        /// Creates the resulting matrix of TF-IDF values.
        /// Reads word by word from tfIdfFilePath and chooses the right index for each value thanks to indexMap.
        /// </summary>
        private float[][] VectorizeData(long height, int width, string tfIdfFilePath) // DO IVectorizeru???
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

        /// <summary>
        /// Reads word by word from word frequency file and creates a new file of TF-IDF values.
        /// </summary>
        private string GetTfIdfFile(string tempTfFilePath)
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

                sw.Write($"{word} {number * (float)Math.Log2(numOfRows / (double)_rowAppearance[word])}"); // WHICH BASE?
                // STOP ABY SE ToString() UKLADALO JAKO 10e-7 NEBO NECO TAKOVYHO
                if (sr.EndOfLine != true) { sw.Write(","); }
                else { sw.Write("\n"); }
            }

            sw.Dispose();
            sr.Dispose();

            return tfIdfFilePath;
        }

        /// <summary>
        /// Reads the initial input word by word and writes down its frequencies.
        /// </summary>
        private void GetFrequencies(IDisposableLineReader rr, string tempTfFilePath)
        {
            var sr = new RecordStreamWordReader(rr, new[]
            {
                ' ', '\t', '\n', '\r', // Whitespace characters
                '!', '"', '#', '$', '%', '&', '\'', '(', ')', // Punctuation marks
                '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{',
                '|', '}', '~' // Symbols
            }); // INTERFACE DEFINOVATELNA ZVENKU
            
            var sw = new FileStreamWriter(tempTfFilePath, true);

            var rowUniqueWordsCount = new Dictionary<string, int>();

            string? word = "";
            while (word is not null)
            {
                int rowWordCount = 0;
                numOfRows++;

                while ((word = sr.ReadWord()) != null)
                {
                    word = word.ToLower();

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


    /// <summary>
    /// Creates a userItemMatrix out of singular recordings of rating.
    /// </summary>
    class UserItemMatrixPreProcessor : IPreProcessor
    {
        public float[][] Preprocess(IDisposableLineReader rr)
        {
            Dictionary<int, Dictionary<int, int>> userRatings = new();
            var sr = new RecordStreamWordReader(rr, new char[] { '\t' }); // TADY LEPSI ROW-READER - ALSO MELO BY TO BYT ZVENKU DEFINABLE

            int userMaxIndex = 0; // To keep track of how big the resulting matrix has to be
            int itemMaxIndex = 0;

            string? word = "";
            while (word is not null) // TOHLE BY MĚL BÝT ZASE NĚJAKEJ SAMOSTATNEJ READER KTEREJ TO DĚLÁ U SEBE (NE VŽDY BUDOU 3 PRVKY A NA STEJNYM MISTE)
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
