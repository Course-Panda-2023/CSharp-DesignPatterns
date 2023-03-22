using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatternsTamas
{
    public class Time
    {
        private static Time mInstance = null;
        public int Seconds { get; private set; } 
        private Time() { }
        const int TimeLimit = 50;

        public static Time Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new Time();
                }

                return mInstance;
            }
        }
        public void Run(string satellitePath, string requestPath)
        {
            this.Seconds = 0;
            ControlUnit cu = new ControlUnit();
            Customer customer = new Customer();
            cu.LoadSatelliteFromFile(satellitePath);
            customer.LoadFutureRequests(requestPath);
            while (this.Seconds < TimeLimit)
            {
                this.EverySecond(cu, customer);
            }
        }
        private void EverySecond(ControlUnit cu, Customer customer)
        {
            Console.WriteLine(++this.Seconds);
            customer.TryToMakeRequests(cu);
            cu.GetDataFromSatellites();
            cu.TryAttachRequests();
            cu.MakeSatellitesTryToSampleData();
        }
        
    }
}
