using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;

namespace RecommendationSystem.Interfaces
{
    internal interface IDataPreprocessor
    {
        float[][] Preprocess(DataFrame dataFrame);
    }




    class TfIdf : IDataPreprocessor
    {
        private Dictionary<string, long> _rowAppearance { get; set; } // In how many rows has the word appeared
        private Dictionary<string, bool> _uniqueWords { get; set; }  // How many unique words are there

        private readonly Dictionary<string, int> _wordIndexMap = new();

        public float[][] Preprocess(DataFrame dataFrame) // vubec nemusi byt 2D  // pouzit Math.Numerics kde jsou efficient matice
        {
            _rowAppearance = new();
            _uniqueWords = new();

            string tempTfFilePath = Path.GetTempFileName();

            GetFrequencies(dataFrame, tempTfFilePath);
            string tfIdfFilePath = GetTfIdfFile(dataFrame, tempTfFilePath);

            File.Delete(tempTfFilePath);

            return VectorizeData(dataFrame.Rows.Count, _uniqueWords.Count, tfIdfFilePath);
        }

        private float[][] VectorizeData(long height, int width, string tfIdfFilePath)
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

                if (!_wordIndexMap.ContainsKey(word)) { _wordIndexMap[word] = _wordIndexMap.Count; } // is Count fast enough? what about a counter

                matrix[i][_wordIndexMap[word]] = number;

                if (sr.EndOfLine && i != height - 1) { i++; matrix[i] = new float[width]; }
            }

            return matrix;
        }

        private string GetTfIdfFile(DataFrame dataFrame, string tempTfFilePath)
        {
            string tfIdfFilePath = "tf_idf.txt";

            var sr = new FileStreamWordReader(tempTfFilePath, new char[] { ',', '\n', '\r', });
            var sw = new FileStreamWriter(tfIdfFilePath, false);

            long numOfRows = dataFrame.Rows.Count;
            string? wordPair;
            while ((wordPair = sr.ReadWord()) != null)
            {
                string[] splitPair = wordPair.Split(' ');

                string word = splitPair[0];
                float number = float.Parse(splitPair[1]);

                sw.Write($"{word} {number * (float)Math.Log10(numOfRows / (double)_rowAppearance[word])}");  // base 10?

                if (sr.EndOfLine != true) { sw.Write(","); }
                else { sw.Write("\n"); }
            }

            sw.Dispose();
            sr.Dispose();

            return tfIdfFilePath;
        }

        private void GetFrequencies(DataFrame dataFrame, string tempTfFilePath)
        {
            var sw = new FileStreamWriter(tempTfFilePath, true);

            var rowUniqueWordsCount = new Dictionary<string, int>();
            foreach (var row in dataFrame.Rows)
            {
                int rowWordCount = 0;

                foreach (var value in row)
                {
                    string loweredOneLineValue = (value.ToString()).ToLower();
                    loweredOneLineValue = Regex.Replace(loweredOneLineValue, @"[,.'–(;)?!:_•\s+-]", " ");
                    loweredOneLineValue = Regex.Replace(loweredOneLineValue, " {2,}", " ");
                    string[] words = loweredOneLineValue.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    foreach (var word in words)
                    {
                        if (!rowUniqueWordsCount.ContainsKey(word)) { rowUniqueWordsCount[word] = 0; }

                        if (rowUniqueWordsCount[word] == 0)
                        {
                            if (!_rowAppearance.ContainsKey(word)) { _rowAppearance[word] = 0; }
                            _rowAppearance[word]++;
                        }

                        _uniqueWords[word] = true;

                        rowUniqueWordsCount[word]++;
                    }

                    rowWordCount += words.Length;
                }

                int keyCounter = 1;
                int numberOfKeys = rowUniqueWordsCount.Keys.Count;
                foreach (var uniqueWord in rowUniqueWordsCount.Keys)
                {
                    sw.Write($"{uniqueWord} {rowUniqueWordsCount[uniqueWord] / (float)rowWordCount}"); // musim u obou (float)?

                    if (keyCounter != numberOfKeys) { sw.Write(","); }
                    else { sw.Write("\n"); }

                    keyCounter++;
                }

                rowUniqueWordsCount.Clear();
            }

            sw.Dispose();
        }
    }
}
