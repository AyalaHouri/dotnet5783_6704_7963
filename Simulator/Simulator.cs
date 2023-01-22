
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Simulator
{

    public static class Simulator
    {
        public static BackgroundWorker orderWorker = new BackgroundWorker();
        internal static readonly Random random = new Random();
        private static BlApi.IBl _bl = BlApi.Factory.Get();
        public static void startsimulator()
        {
            orderWorker.DoWork += simulator_DoWork;

            orderWorker.WorkerReportsProgress = true;

            orderWorker.WorkerSupportsCancellation = true;

            orderWorker.RunWorkerAsync(1);

        }
        public static void stopsimulator()
        {
            /// Thread. 
        }

        private static void simulator_DoWork(object sender, DoWorkEventArgs e)
        {
            int? id = _bl.Order.ordertosimulator();
            while (!orderWorker.CancellationPending && id != null)
            {
                int y = random.Next(3, 10);
                Thread.Sleep(1000);


                BO.Order order = _bl.Order.OrderDetail(id ?? 0);
                if (order.OrderStatus.ToString() == "shipped")
                {
                    order = _bl.Order.UpdateStock(order.ID);
                }
                if (order.OrderStatus.ToString() == "OrderConfirmed")
                {
                    order = _bl.Order.ordershipdateupdate(order.ID);
                }
                id = _bl.Order.ordertosimulator();
                orderWorker.ReportProgress(y);
                /// updateOrder();
            }
        }
        //private static void updateOrder()
        //{
        //   

        //    while ()
        //    {

        //    }

        //}

    }
}