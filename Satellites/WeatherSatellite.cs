using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteControl
{
    class WeatherSatellite : Satellite
    {
        public WeatherSatellite(string[] sInfo) : base(sInfo) { }

        public override void SampleData()
        {
            foreach (Request r in requests)
            {
                string convertedData = "";
                int[] numeralData = r.Data.ToCharArray().Select((c) => ((int)(c >= 'a' ? c + 'A' - 'a' : c))).ToArray();
                for (int i = 0; i < numeralData.Length; i++)
                {
                    convertedData += (char)numeralData[i];
                }
                r.ProcessedData = convertedData;
            }
        }
    }
}
