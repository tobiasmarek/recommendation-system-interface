using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Data.Analysis;

namespace RecommendationSystem.Interfaces
{
    internal interface IVectorizer<T>
    {
        float[] Vectorize(T data, DataFrame dataFrame);
    }


    interface IRowVectorizer
    {
        float[] VectorizeRow();
    }


    interface IUserVectorizer
    {
        float[] VectorizeUser(User user, DataFrame dataFrame);
    }


    interface IUserAndRowVectorizer : IUserVectorizer, IRowVectorizer { }


    class UserAndRowVectorizer : IUserAndRowVectorizer, IDataPreprocessor
    {
        private readonly bool _isPreprocessed = false;
        private string _preprocessedFilePath;
        private int _numberOfUniqueWords;

        private readonly Dictionary<string, int> _wordIndexMap = new Dictionary<string, int>();

        private FileStreamWordReader _sr;
        public float[] VectorizeRow() // VectorizeNextRow? taky musim predavat DataFrame (a row je uplně k ničemu tady)
        {
            var vector = new float[_numberOfUniqueWords];

            string? wordPair;
            while ((wordPair = _sr.ReadWord()) != null)
            {
                string[] splitPair = wordPair.Split(' ');

                string word = splitPair[0];
                float number = float.Parse(splitPair[1]);

                if (!_wordIndexMap.ContainsKey(word)) { _wordIndexMap[word] = _wordIndexMap.Count; } // is Count fast enough? what about a counter

                vector[_wordIndexMap[word]] = number;

                if (_sr.EndOfLine != true) { break; }
            }

            return vector;
        }

        public float[] VectorizeUser(User user, DataFrame dataFrame) // musim porad predavat dataFrame?
        {
            if (_isPreprocessed == false)
            {
                Preprocess(dataFrame);
                _sr = new FileStreamWordReader(_preprocessedFilePath, new char[] { ',', '\n', '\t', '\r', '\v', '\f' });
            }

            var vector = new float[_numberOfUniqueWords];

            return vector;
        }

        public void Preprocess(DataFrame dataFrame)
        {
            var rowAppearance = new Dictionary<string, long>(); // In how many rows has the word appeared
            var uniqueWords = new Dictionary<string, bool>();  // How many unique words are there

            var rowUniqueWordsCount = new Dictionary<string, int>();
            string tempTfFilePath = Path.GetTempFileName();
            var sw = new FileStreamWriter(tempTfFilePath, true);
            foreach (var row in dataFrame.Rows)
            {
                int rowWordCount = 0;

                foreach (var value in row)
                {
                    string loweredOneLineValue = ((string)value).ToLower().Replace(Environment.NewLine, " ");
                    string[] words = loweredOneLineValue.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    foreach (var word in words)
                    {
                        if (rowUniqueWordsCount[word] == 0) { rowAppearance[word]++; }

                        uniqueWords[word] = true;

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

            var sr = new FileStreamWordReader(tempTfFilePath, new char[] {',', '\n', '\t', '\r', '\v', '\f'});
            sw = new FileStreamWriter($"tf_idf.txt");

            long numOfRows = dataFrame.Rows.Count;
            string? wordPair;
            while ((wordPair = sr.ReadWord()) != null)
            {
                string[] splitPair = wordPair.Split(' ');
                
                string word = splitPair[0];
                float number = float.Parse(splitPair[1]);
                
                sw.Write($"{word} {number * (float)Math.Log10(numOfRows / (double)rowAppearance[word])}");  // base 10?
                
                if (sr.EndOfLine != true) { sw.Write(","); }
                else { sw.Write("\n"); }
            }

            sw.Dispose();
            sr.Dispose();

            File.Delete(tempTfFilePath);

            _numberOfUniqueWords = uniqueWords.Count;
            _preprocessedFilePath = "tf_idf.txt";
        }
    }
}
