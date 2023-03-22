using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTamas
{
    public class PhotoSatellite : IsraeliSatellite
    {
        public PhotoSatellite(string id, int elapse_time, int start_time) : base(id, elapse_time, start_time)
        {}
        
        public override void SampleData()
        {
            base.data = base.Location.ToLower();
            Console.WriteLine("sampled data: " + this.Location + " to " + string.Concat(Enumerable.Repeat("?", this.Location.Length)));
        }
        public override string ToString()
        {
            return $"PhotoSatellite(Id={Id}, Elapse_time={Elapse_time}, Start_time={Start_time}, Location={Location}, data={data})";
        }
    }
}
