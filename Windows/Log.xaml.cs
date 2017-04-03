using System.Windows;

namespace BatchRenamer.Windows
{
    public partial class Log : Window
    {
        public static bool IsOpen { get; private set; }

        public Log()
        {
            InitializeComponent();

            LogListBox.ItemsSource = Logger.Instance.GetLog;

            LogListBox.Items.SortDescriptions.Add(
                new System.ComponentModel.SortDescription("Timestamp", System.ComponentModel.ListSortDirection.Descending));

            Loaded += Log_Loaded;
            Closing += Log_Closing;
        }

        private void Log_Loaded(object sender, RoutedEventArgs e)
        {
            IsOpen = true;
        }

        private void Log_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IsOpen = false;
        }
    }
}
