using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ClipboardMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isTimerPaused = false;

        private DispatcherTimer _timer;

        private Queue<string> clipboardQueue = new Queue<string>();

        public MainWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(AddToList_Tick);
            _timer.Interval = new TimeSpan(0, 0, 2); // every 2 secs.
            _timer.Start();
        }

        private void AddToList_Tick(object sender, EventArgs e)
        {
            var item = Clipboard.GetText();

            if (item != "" && item != null && !clipboardQueue.Contains(item))
            {
                clipboardQueue.Enqueue(item);
            }

            if (clipboardQueue.Count > 5)
            {
                clipboardQueue.Dequeue();
            }

            ClipBoardListView.ItemsSource = clipboardQueue.Reverse().ToList();
        }

        #region UI        

        private void PauseClock_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerPaused)
            {
                isTimerPaused = false;
                _timer.Stop();
            }
            else
            {
                isTimerPaused = true;
                _timer.Start();
            }
        }

        private void CopyContent_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            string copiedItem = sender as string;

            if (copiedItem != null)
            {
                Clipboard.SetDataObject(copiedItem);
            }
        }
        #endregion


    }
}
