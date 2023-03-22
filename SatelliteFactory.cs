using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTamas
{
    internal class SatelliteFactory
    {
        //public SatelliteFactory() { }
        public static ISatellite Create(string type, string id, int elapse_time, int start_time)
        {
            if (type == "BW") 
            {
                return new PhotoSatellite(id, elapse_time, start_time);
            }
            else if (type == "W")
            {
                return new WeatherSatellite(id, elapse_time, start_time);
            }
            else if (type == "WV")
            {
                return new WorldViewAdapter(id, elapse_time, start_time);  
            }
            else { throw new ArgumentException(); }
        }
        public static ISatellite Create(NewSatelliteStats stats)
        {
            return Create(stats.Type, stats.Id, stats.Elapse_time, stats.Start_time);
        }
    }
}
