using System;
using System.Collections.ObjectModel;

namespace BatchRenamer
{
    // From http://csharpindepth.com/Articles/General/Singleton.aspx [Sixth version - using .NET 4's Lazy<T> type]
    public sealed class Logger
    {
        private static readonly Lazy<Logger> lazy =
            new Lazy<Logger>(() => new Logger());

        public static Logger Instance { get { return lazy.Value; } }

        public ObservableCollection<Models.Log> GetLog { get => log; }

        private ObservableCollection<Models.Log> log;

        private Logger()
        {
            log = new ObservableCollection<Models.Log>();
        }

        public void WriteLog(string logThis)
        {
            log.Add(new Models.Log(DateTime.Now, logThis));
        }
    }
}
