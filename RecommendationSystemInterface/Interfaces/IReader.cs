using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace RecommendationSystemInterface.Interfaces
{
    /// <summary>
    /// Reads a line.
    /// </summary>
    /// <returns>Returns null if it reached the end of the input, else the string</returns>
    public interface ILineReader
    {
        string? ReadLine();
    }




    /// <summary>
    /// LineReader that implements Dispose function.
    /// </summary>
    public interface IDisposableLineReader : ILineReader, IDisposable { }


    /// <summary>
    /// Reads line by line from a Stream.
    /// </summary>
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




    /// <summary>
    /// Reads a word.
    /// </summary>
    /// <returns>Returns null if it reached the end of the input, else the word</returns>
    interface IWordReader
    {
        string? ReadWord();
    }




    /// <summary>
    /// WordReader that implements Dispose function.
    /// </summary>
    interface IDisposableWordReader : IWordReader, IDisposable
    {
        public bool EndOfLine { get; set; }
    }


    /// <summary>
    /// Reads word by word from a Stream.
    /// </summary>
    class FileStreamWordReader : IDisposableWordReader
    {
        public bool EndOfLine { get; set; }

        private readonly StreamReader _sr;
        private readonly char[] _separators;
        private readonly StringBuilder _sb;
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

            if (_wordStarted)
            {
                word = _sb.ToString();
                _wordStarted = false;
                _sb.Clear();
                return word;
            }
            
            if (word is not null) { return word; }

            return null;
        }

        public void Dispose() { _sr.Dispose(); }
    }


    /// <summary>
    /// Reads word by word from a StringStream.
    /// </summary>
    class StringStreamWordReader : IDisposableWordReader // NENI TO REDUNDANT - CO OBECNEJ STREAMWORDREADER A TAM DODÁVAT DO TextReaderu?
    { 
        public StringReader Sr { get; set; }
        public bool EndOfLine { get; set; }

        private readonly char[] _separators;
        private readonly StringBuilder _sb;
        private bool _wordStarted;

        public StringStreamWordReader(char[] separators)
        {
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
                while ((chNumber = Sr.Read()) != -1)
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

            if (_wordStarted)
            {
                word = _sb.ToString();
                _wordStarted = false;
                _sb.Clear();
                return word;
            }

            if (word is not null) { return word; }

            return null;
        }

        public void Dispose() { Sr.Dispose(); }
    }


    /// <summary>
    /// A factory programming pattern.
    /// Takes input from somewhere, converts it to a string and uses StringStreamWordReader to read word by word.
    /// </summary>
    class RecordStreamWordReader : StringStreamWordReader // NEBO NEJAKEJ EndOfRowStringWordReader
    {
        private string? _row;
        private string? _nextWord = null;
        private readonly IDisposableLineReader _rr;

        public RecordStreamWordReader(IDisposableLineReader rr, char[] separators) : base(separators)
        {
            EndOfLine = true;
            _rr = rr;
            _row = String.Empty;
        }

        public new string? ReadWord()
        {
            string? word;
            if (EndOfLine == true)
            {
                _row = _rr.ReadLine();
                if (_row == null) { _rr.Dispose(); return null; }
                Sr = new StringReader(_row);
                EndOfLine = false;
            }

            if (_nextWord == null) { word = base.ReadWord(); }
            else { word = _nextWord; }

            if (word == null) { return ""; }

            _nextWord = base.ReadWord();
            if (_nextWord == null) { EndOfLine = true; }

            return word;
        }
    }
}
