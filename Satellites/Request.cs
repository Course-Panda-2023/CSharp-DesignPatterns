using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteControl
{
    public class Request
    {
        private string type;
        private string data;
        private string processedData;
        private int onTime;

        public string Type { get { return type; } }
        public string Data { get { return data; } set { data = value; } }
        public string ProcessedData { get { return processedData; } set { processedData = value; } }
        public int OnTime { get { return onTime; } } 
        public Request(string type, string data, int onTime)
        {
            this.type = type;
            this.data = data;
            this.processedData = "";
            this.onTime = onTime;
        }
    }
}
