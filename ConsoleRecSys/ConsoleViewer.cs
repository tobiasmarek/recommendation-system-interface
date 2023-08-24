using System;
using RecommendationSystemInterface;

namespace ConsoleRecSys
{
    class ConsoleViewer : Viewer
    {
        public override void ViewFile(string filePath)
        {
            string[] lines = ReadFirstKLines(filePath, 5);

            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }

        public override void ViewString(string str)
        {
            Console.WriteLine(str);
        }
    }
}
