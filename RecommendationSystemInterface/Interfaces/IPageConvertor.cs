using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RecommendationSystemInterface.Interfaces
{
    /// <summary>
    /// Should take the result file that contains indexed results and convert them to human readable form.
    /// Should use a paging method. With each ConvertPage call, it should return, page after page, the whole result file.
    /// </summary>
    public interface IPageConvertor
    {
        FileStreamWordReader ResultReader { get; init; }
        string ConvertPage();
    }



    /// <summary>
    /// Takes a result file and, according to its template file, convert it to human readable form.
    /// The form that the result file should have is a CSV file where every line is a user and every index is rated like this "[index] [rating]"
    /// Retrieves lines from the given template document. Indices are specified in the result file.
    /// </summary>
    class FileConvertor : IPageConvertor
    {
        public FileStreamWordReader ResultReader { get; init; }

        private readonly string _templateFilePath;
        private readonly StringBuilder _sb;
        private readonly int _chunkSize;

        public FileConvertor(string resultFilePath, string templateFilePath, int chunkSize = 20)
        {
            try
            {
                ResultReader = new FileStreamWordReader(resultFilePath, new[] {','});
            }
            catch
            {
                throw new LoggerException("Problem has occurred when creating FileStreamWordReader in FileConvertor");
            }

            _templateFilePath = templateFilePath;
            _sb = new StringBuilder();
            _chunkSize = chunkSize;
        }

        public string ConvertPage()
        {
            _sb.Clear();

            var lineIndicesAndRatings = GetNextLineIndicesAndRatings();

            var sortedLineIndicesList = lineIndicesAndRatings.Keys.ToList();
            // Sorts it so that the resulting lines are presented in descending order of their rating (from best to worst)
            sortedLineIndicesList.Sort((a, b) => lineIndicesAndRatings[b].CompareTo(lineIndicesAndRatings[a]));

            var foundLines = GetLines(lineIndicesAndRatings);

            foreach (int index in sortedLineIndicesList)
            {
                if (foundLines.ContainsKey(index))
                {
                    _sb.Append($"Index: {index}; Rating: **{lineIndicesAndRatings[index]}**; ");

                    if (foundLines[index].Length > 300)
                    {
                        _sb.Append($"{foundLines[index][..300]}...");
                    }
                    else
                    {
                        _sb.Append($"{foundLines[index]}");
                    }
                }
                else
                {
                    _sb.Append($"Line {index} not found.");
                }

                _sb.Append($"{Environment.NewLine}{Environment.NewLine}");
            }

            return _sb.ToString();
        }

        /// <summary>
        /// Gets a chunk of the result's file pairs and sorts it according to indices
        /// so that we can retrieve lines just by reading through the template file without returning.
        /// </summary>
        private SortedDictionary<int, int> GetNextLineIndicesAndRatings()
        {
            var resultDictionary = new SortedDictionary<int, int>();

            int wordCounter = 0;
            string? pair;
            while ((pair = ResultReader.ReadWord()) is not null && !ResultReader.EndOfLine && wordCounter <= _chunkSize)
            {
                string[] stringPair = pair.Split(' ');

                if (stringPair.Length == 2 && int.TryParse(stringPair[0], out int itemIndex) &&
                    int.TryParse(stringPair[1], out int itemValue))
                {
                    resultDictionary.Add(itemIndex, itemValue);
                }

                wordCounter++;
            }

            return resultDictionary;
        }

        /// <summary>
        /// Retrieves lines with specified index from the template document.
        /// </summary>
        private Dictionary<int, string> GetLines(SortedDictionary<int, int> lineIndicesAndRatings)
        {
            int[] sortedLineIndices = lineIndicesAndRatings.Keys.ToArray();

            Dictionary<int, string> foundLines = new();

            try
            {
                using (var templateReader = new StreamReader(_templateFilePath))
                {
                    int lineNumber = 0;
                    int index = 0;

                    string? line;
                    while ((line = templateReader.ReadLine()) is not null && index < sortedLineIndices.Length)
                    {
                        if (lineNumber == sortedLineIndices[index])
                        {
                            foundLines.Add(sortedLineIndices[index], line);
                            index++;
                        }

                        lineNumber++;
                    }
                }
            }
            catch
            {
                throw new LoggerException("Problem when trying to read in templateReader of Convertor!");
            }

            return foundLines;
        }
    }




    /// <summary>
    /// Convertor from a result file to human readable form according to its template file that is present in a database.
    /// </summary>
    class ContentBasedDbsConvertor : IPageConvertor
    {
        public FileStreamWordReader ResultReader { get; init; }

        public ContentBasedDbsConvertor(string resultFilePath)
        {
            try
            {
                ResultReader = new FileStreamWordReader(resultFilePath, new[] {','});
            }
            catch
            {
                throw new LoggerException("Problem has occurred when creating FileStreamWordReader in DBS Convertor");
            }
        }

        public string ConvertPage()
        {
            throw new NotImplementedException();
        }
    }
}
