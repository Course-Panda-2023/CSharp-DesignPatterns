using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPExcersice.FileManager
{
    public class NewSateliteRequest
    {
        public string SateliteType;
        public string Id;
        public int ElapseTime;
        public int StartTime;


        public NewSateliteRequest(string sateliteType, string id, int elapseTime, int startTime)
        {
            SateliteType = sateliteType;
            Id = id;
            ElapseTime = elapseTime;
            StartTime = startTime;
        }
    }
}
