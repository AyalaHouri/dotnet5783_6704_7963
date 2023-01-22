using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace PL
{
    /// <summary>
    /// Interaction logic for sinulator.xaml
    /// </summary>
    public partial class sinulator : Window
    {

        BackgroundWorker clockWorker;

        DateTime display = DateTime.Now;
        private int rate = 1;

        private BlApi.IBl _bl = BlApi.Factory.Get();
        private ObservableCollection<BO.OrderForList> collection { get; set; }
        public sinulator()
        {
            InitializeComponent();
            stop.IsEnabled = false;
            collection = new ObservableCollection<BO.OrderForList>(_bl.Order.AskList());
            orders.DataContext = collection;
            ShowTime();
            clockWorker = new BackgroundWorker();
            clockWorker.DoWork += ClockWorker_DoWork;

            clockWorker.ProgressChanged += ClockWorker_UpdateDisplay;
            clockWorker.WorkerReportsProgress = true;

            clockWorker.WorkerSupportsCancellation = true;
            clockWorker.RunWorkerCompleted += ClockWorker_RunWorkerCompleted;

            clockWorker.RunWorkerAsync(1);

        }
        private void ClockWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int sec = rate;
            clockWorker.RunWorkerAsync(sec);
        }
        private void ShowTime()
        {
            string timerText = display.ToString();
            ///timerText = timerText.Substring(10, 18);
            timeTextBlock.Text = timerText;
        }
        private void ClockWorker_UpdateDisplay(object sender, ProgressChangedEventArgs e)
        {
            display = display.AddSeconds(e.ProgressPercentage);
            ShowTime();
        }
        private void ClockWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int sec = (int)e.Argument; // sec = 1
            Simulator.Simulator.orderWorker.ProgressChanged += OrderWorker_ProgressChanged;
            while (!clockWorker.CancellationPending) // while (true);
            {
                Thread.Sleep(1000);
                clockWorker.ReportProgress(sec);
            }
        }
        private void OrderWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            collection = new ObservableCollection<BO.OrderForList>(_bl.Order.AskList());
            orders.DataContext = collection;
            orders.Items.Refresh();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            Simulator.Simulator.startsimulator();
            start.IsEnabled = false;
            stop.IsEnabled = true;
        }
        private void tracking_Click(object sender, RoutedEventArgs e)
        {
            Button button = (sender as Button);
            int x = (int)button.Tag;
            new trackingorder(x).Show();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            Simulator.Simulator.orderWorker.CancelAsync();
            start.IsEnabled = true;
            stop.IsEnabled = false;
        }
        //private void Slider_ValueChanged(object sender, RoutedEventArgs e, Slider slider)
        //{
        //    Button button = (sender as Button);
        //    if ((string)button.Tag == "OrderConfirmed")
        //        slider.Value = 1;
        //    if ((string)button.Tag == "shipped")
        //        slider.Value = 2;
        //    if ((string)button.Tag == "deliveredToCustomer")
        //        slider.Value = 3;

        //}


    } 
}