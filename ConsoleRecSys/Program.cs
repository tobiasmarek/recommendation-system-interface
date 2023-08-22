using System;
using RecommendationSystemInterface;

namespace ConsoleRecSys
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var session = new ConsoleSession();
        }
    }


    class ConsoleSubjectSession : ConsoleSession
    {
        public void AddFavourite() // tady spis ne, nebo ne?
        {

        }
    }
}