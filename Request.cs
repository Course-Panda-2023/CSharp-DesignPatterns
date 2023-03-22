using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTamas
{
    public class Request
    {
        public char Type { get; }
        public string Location { get; }
        public int Time { get; }
        public bool IsAssigned { get; set; }

        public Request(char type, string location, int time) 
        {
            this.Type = type;
            this.Location = location;
            this.Time = time;
            this.IsAssigned = false;
        }
        public override string ToString()
        {
            return $"Request(Type={Type}, Location={Location}, Time={Time}, IsAssigned={IsAssigned})";
        }
    }
}
