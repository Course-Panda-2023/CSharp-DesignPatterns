using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPExcersice.FileManager
{
    public class NewActionRequest
    {
        public string SateliteType;
        public string Location;
        public int OnTime;

        public NewActionRequest(string sateliteType, string location, int onTime)
        {
            SateliteType = sateliteType;
            Location = location;
            OnTime = onTime;
        }
        public NewActionRequest(string sateliteType, string location)
        {
            SateliteType = sateliteType;
            Location = location;
        }
    }
}
