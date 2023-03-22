using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTamas
{
    internal class NewSatelliteStats
    {
        public string Type { get; }
        public string Id { get; }
        public int Elapse_time { get; }
        public int Start_time { get; }
        public NewSatelliteStats(string type, string id, int elapse_time, int start_time)
        {
            Type = type;
            Id = id;
            Elapse_time = elapse_time;
            Start_time = start_time;
        }
    }
}
