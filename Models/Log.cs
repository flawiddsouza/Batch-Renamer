using System;

namespace BatchRenamer.Models
{
    public class Log
    {
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }

        public Log(DateTime timestamp, string details)
        {
            Timestamp = timestamp;
            Details = details;
        }
    }
}
