using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystemInterface.Interfaces
{
    public interface IPageConvertor
    {
        FileStreamWordReader ResultReader { get; init; }
        string ConvertPage();
    }




    class FileConvertor : IPageConvertor
    {
        public FileStreamWordReader ResultReader { get; init; }

        private readonly string _templateFilePath;
        private readonly StringBuilder _sb;
        private readonly int _chunkSize;

        public FileConvertor(string resultFilePath, string templateFilePath, int chunkSize = 10)
        {
            try
            {
                ResultReader = new FileStreamWordReader(resultFilePath, new[] {','});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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
            sortedLineIndicesList.Sort((a, b) => lineIndicesAndRatings[b].CompareTo(lineIndicesAndRatings[a]));

            var foundLines = GetLines(lineIndicesAndRatings);

            foreach (int index in sortedLineIndicesList)
            {
                if (foundLines.ContainsKey(index))
                {
                    _sb.Append($"Index: {index}; Rating: **{lineIndicesAndRatings[index]}**; ");

                    if (foundLines[index].Length > 300)
                    {
                        _sb.Append($"{foundLines[index].Substring(0, 300)}...");
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
            }

            return resultDictionary;
        }

        private Dictionary<int, string> GetLines(SortedDictionary<int, int> lineIndicesAndRatings)
        {
            int[] sortedLineIndices = lineIndicesAndRatings.Keys.ToArray();

            Dictionary<int, string> foundLines = new Dictionary<int, string>();

            using (StreamReader templateReader = new StreamReader(_templateFilePath))
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

            return foundLines;
        }
    }




    class ContentBasedDbsConvertor : IPageConvertor
    {
        public FileStreamWordReader ResultReader { get; init; }

        public ContentBasedDbsConvertor(string resultFilePath)
        {
            try
            {
                ResultReader = new FileStreamWordReader(resultFilePath, new[] {','});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string ConvertPage()
        {
            throw new NotImplementedException();
        }
    }
}
