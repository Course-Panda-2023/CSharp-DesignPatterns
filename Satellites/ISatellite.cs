using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteControl
{
    interface ISatellite
    {
        public int Update();
        public int GetNextTime(int currentTime);
        public void SetCommand(Request location);
        public List<Request> GetData();
        public void SampleData();
    }
}
