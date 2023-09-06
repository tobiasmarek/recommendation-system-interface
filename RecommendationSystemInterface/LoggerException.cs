using System;

namespace RecommendationSystemInterface
{
    /// <summary>
    /// Specific exception that works as a logger that can be
    /// caught by try catch statement
    /// </summary>
    public class LoggerException : Exception
    {
        public LoggerException(string message) : base(message) { }
    }
}
