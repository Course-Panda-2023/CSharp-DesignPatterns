using SatelliteControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteControl
{
    // ADAPTER DESIGN PATTERN
    class WVAdapter : PhotoSatellite
    {
        private WorldViewSatellite mAdaptee;

        public WVAdapter(string[] sInfo) : base(sInfo)
        {
            mAdaptee = new WorldViewSatellite();
        }

        public override void SampleData()
        {
            const int PrevLowerCaseA = 129;
            const int ConvertToLowerCase = 32;

            foreach (Request r in requests)
            {
                string processedData = "";
                mAdaptee.SetLocation(r.Data);
                mAdaptee.SampleData();
                int[] data = mAdaptee.View();
                if (data != null)
                {
                    // Corrects the data to the format we are using.
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] >= PrevLowerCaseA)
                            processedData += Convert.ToChar(data[i] - ConvertToLowerCase);
                        else
                            processedData += Convert.ToChar(data[i] + ConvertToLowerCase);
                    }
                }
                r.ProcessedData = processedData;
            }
        }
    }
}
