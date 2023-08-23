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
        public abstract void View(string filePath); // POKUD NEBUDE MIT NIC VIC NEZ VIEW, MEL BYCH ZVAZIT INTERFACE

        public string[] ReadFirstKLines(string filePath, int k)
        {
            string[] lines = new string[k];
            using (var reader = new StreamReader(filePath))
            {
                for (int i = 0; i < k; i++)
                {
                    string? line = reader.ReadLine();

                    if (line == null) { break; }

                    lines[i] = line;
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
        public override void View(string filePath)
        {
            string[] lines = ReadFirstKLines(filePath, 5);

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }


    /// <summary>
    /// A Viewer which output is shown on the web.
    /// </summary>
    class WebViewer : Viewer
    {
        public override void View(string filePath)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// A Viewer which result is shown in Windows Forms app.
    /// </summary>
    class WinFormsViewer : Viewer
    {
        public override void View(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
