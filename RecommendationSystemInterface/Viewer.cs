using System;
using System.Diagnostics;
using System.IO;

namespace RecommendationSystemInterface
{
    /// <summary>
    /// Views results and all the processes that are running in the current Session.
    /// </summary>
    public abstract class Viewer
    {
        public abstract void ViewFile(string filePath);

        public abstract void ViewString(string str);

        public string[] ReadFirstKLines(string filePath, int k)
        {
            string[] lines = new string[k];
            using (var reader = new StreamReader(filePath))
            {
                for (int i = 0; i < k; i++)
                {
                    try
                    {
                        string? line = reader.ReadLine();
                        if (line == null) { break; }

                        if (line.Length > 300)
                        {
                            lines[i] = $"{line.Substring(0, 300)}...";
                        }
                        else
                        {
                            lines[i] = line;
                        }
                    }
                    catch
                    {
                        throw new CustomException("Problem when trying to read a line in Viewer's StreamReader!");
                    }
                }
            }
            return lines;
        }
    }




    /// <summary>
    /// A Viewer which output is directed to the Console.
    /// </summary>
    class ConsoleViewer : Viewer
    {
        public override void ViewFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public override void ViewString(string str)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// A Viewer which output is shown on the web.
    /// </summary>
    class WebViewer : Viewer
    {
        public override void ViewFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public override void ViewString(string str)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// A Viewer which result is shown in Windows Forms app.
    /// </summary>
    class WinFormsViewer : Viewer
    {
        public override void ViewFile(string filePath)
        {
            throw new NotImplementedException();
        }

        public override void ViewString(string str)
        {
            throw new NotImplementedException();
        }
    }
}
