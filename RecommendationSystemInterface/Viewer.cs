using System;
using System.Diagnostics;
using System.IO;

namespace RecommendationSystemInterface
{
    public abstract class Viewer // neni to přehrocený? nemam jen dát jednu class a předávat kam se to zobrazuje?
                          // nebo předávat interface? nebo dokonce pouze interface?
    {
        public abstract void View(string filePath);

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


    class WebViewer : Viewer
    {
        public override void View(string filePath)
        {
            try
            {
                // Read all lines from the file
                string[] lines = ReadFirstKLines(filePath, 5);

                // Prepare the content with paragraph tags
                string content = string.Join("\n", lines);
                content = "<html><head></head><body><p>" + content.Replace("\n", "</p><p>") + "</p></body></html>";

                // Save the content to a temporary HTML file
                string tempHtmlFilePath = Path.GetTempFileName() + ".html";
                File.WriteAllText(tempHtmlFilePath, content);

                // Open the default web browser to display the content
                Process.Start(tempHtmlFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }


    class WinFormsViewer : Viewer
    {
        public override void View(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
