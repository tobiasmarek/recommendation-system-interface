using System;
using RecommendationSystemInterface;

namespace ConsoleRecSys
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var viewer = new ConsoleViewer();
            var session = new ConsoleSession(viewer);
            var controller = new ConsoleController(session);
        }
    }
}