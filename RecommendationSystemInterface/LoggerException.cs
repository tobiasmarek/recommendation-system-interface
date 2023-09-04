using System;

namespace RecommendationSystemInterface
{
    public class LoggerException : Exception
    {
        public LoggerException() { }

        public LoggerException(string message) : base(message) { }
    }
}
