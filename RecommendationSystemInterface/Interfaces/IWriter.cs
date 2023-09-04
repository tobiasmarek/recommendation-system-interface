using System;
using System.IO;

namespace RecommendationSystemInterface.Interfaces
{
    /// <summary>
    /// Writes a string.
    /// </summary>
    internal interface IWriter
    {
        void Write(string value);
    }




    /// <summary>
    /// IWriter with Dispose functionality.
    /// </summary>
    interface IDisposableWriter : IWriter, IDisposable { }


    /// <summary>
    /// Writes a string with StreamWriter with a specified path. 
    /// </summary>
    class FileStreamWriter : IDisposableWriter
    {
        private readonly StreamWriter _sw;

        public FileStreamWriter(string path, bool appending = true)
        {
            try
            {
                _sw = new StreamWriter(path, appending);
            }
            catch
            {
                throw new LoggerException("Problem has occurred when creating FileStreamWriter");
            }
        }

        public void Write(string value)
        {
            try
            {
                _sw.Write(value);
            }
            catch
            {
                throw new LoggerException("Problem has occurred when trying to write in FileStreamWriter");
            }
        }

        public void Dispose() { _sw.Dispose(); }
    }
}
