using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTamas
{
    public class WeatherSatellite : IsraeliSatellite
    {
        public WeatherSatellite(string id, int elapse_time, int start_time) : base(id, elapse_time, start_time)
        {}
        public override void SampleData()
        {
            Console.WriteLine("sampled data: " + this.Location + " to " + string.Concat(Enumerable.Repeat("?", this.Location.Length)));
            base.data = base.Location.ToUpper();
        }
        public override string ToString()
        {
            return $"WeatherSatellite(Id={Id}, Elapse_time={Elapse_time}, Start_time={Start_time}, Location={Location}, data={data}";
        }
    }
}
