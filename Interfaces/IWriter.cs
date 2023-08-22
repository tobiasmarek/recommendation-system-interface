using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RecommendationSystem.Interfaces
{
    internal interface IWriter
    {
        void Write(string value);
    }




    interface IDisposableWriter : IWriter, IDisposable { }


    class FileStreamWriter : IDisposableWriter
    {
        private readonly StreamWriter _sw;

        public FileStreamWriter(string path, bool appending = true)
        {
            try
            {
                _sw = new StreamWriter(path, appending);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Write(string value)
        {
            try
            {
                _sw.Write(value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Dispose() { _sw.Dispose(); }
    }
}
