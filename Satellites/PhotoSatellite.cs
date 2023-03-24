using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteControl
{
    class PhotoSatellite : Satellite
    {
        public PhotoSatellite(string[] sInfo) : base(sInfo) { }

        public override void SampleData()
        {
            // Returns the data as an lowercase string.
            foreach(Request r in requests)
            {
                string convertedData = "";
                int[] numeralData = r.Data.ToCharArray().Select((c) => ((int)(c >= 'a' ? c : c + 'a' - 'A'))).ToArray();
                for (int i = 0; i < numeralData.Length; i++)
                {
                    convertedData += (char)numeralData[i];
                }
                r.ProcessedData = convertedData;
            }
        }
    }
}
