using System;
using System.Text;
using RecommendationSystemInterface;

namespace ConsoleRecSys
{
    /// <summary>
    /// A Viewer which output is directed to the Console.
    /// </summary>
    public class ConsoleViewer : Viewer
    {
        private const string ErrorMsg = "Wrong command format - should be '";

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

        public void ShowIndexedArray(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine($"{i}: {array[i]}");
            }
        }

        /// <summary>
        /// Constructs an error message about wrong command
        /// and shows which parameters should be used to make it work properly.
        /// </summary>
        public void WrongCmdErrorMsg(string[] parameters)
        {
            if (parameters.Length == 0) { return; }

            var sb = new StringBuilder();
            sb.Append(ErrorMsg);
            sb.Append(parameters[0]);

            for (int i = 1; i < parameters.Length; i++)
            {
                sb.Append($" [{parameters[i]}]");
            }

            sb.Append('\'');

            Console.WriteLine(sb.ToString());
        }
    }
}
