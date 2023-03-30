using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPExcersice.Satelites
{
    public class IDFWeatherSatellite : Satellite, ISatellite
    {
        public IDFWeatherSatellite()
        {
        }

        public IDFWeatherSatellite(string id, int elaspsedTime, int startTime)
        {
            Id = id;
            StartTime = startTime;
            ElaspsedTime = elaspsedTime;
            StartTime = startTime;
            SingleTon.SingletonTime.Instance.Attach(this);
        }
        public override void SampleData()
        {
            Data = Location.ToUpper();
        }
    }
}
