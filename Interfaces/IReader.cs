using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationSystem.Interfaces
{
    internal interface ILineReader
    {
        string? ReadLine();
    }




    interface IDisposableLineReader : ILineReader, IDisposable { }


    class FileStreamLineReader : IDisposableLineReader
    {
        private readonly StreamReader _sr;

        public FileStreamLineReader(string path)
        {
            try
            {
                _sr = new StreamReader(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string? ReadLine()
        {
            try
            {
                return _sr.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Dispose() { _sr.Dispose(); }
    }




    interface IWordReader
    {
        string? ReadWord();
    }




    interface IDisposableWordReader : IWordReader, IDisposable { }


    class FileStreamWordReader : IDisposableWordReader
    {
        public bool EndOfLine;

        private readonly StreamReader _sr;
        private readonly char[] _separators;
        private StringBuilder _sb;
        private bool _wordStarted;

        public FileStreamWordReader(string path, char[] separators)
        {
            try
            {
                this._sr = new StreamReader(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            this.EndOfLine = false;
            this._separators = separators;
            this._sb = new StringBuilder();
            this._wordStarted = false;
        }

        public string? ReadWord()
        {
            string? word = null;
            EndOfLine = false;

            try
            {
                int chNumber;
                char ch;
                while ((chNumber = _sr.Read()) != -1)
                {
                    ch = (char)chNumber;

                    if (ch == '\n') { EndOfLine = true; }

                    if (_separators.Contains(ch))
                    {
                        if (_wordStarted)
                        { 
                            word = _sb.ToString();
                            _wordStarted = false;
                            _sb.Clear();
                        }
                    }
                    else
                    {
                        _sb.Append(ch);

                        _wordStarted = true;

                        if (word is not null) { return word; }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (_wordStarted) { return _sb.ToString(); }
            
            if (word is not null) { return word; }

            return null;
        }

        public void Dispose() { _sr.Dispose(); }
    }
}
