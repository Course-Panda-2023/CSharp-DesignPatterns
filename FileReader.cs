using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTamas
{
    internal class FileReader
    {
        public static NewSatelliteStats[] ReadSatelliteStats(string path)
        {
            string[] fileLines = System.IO.File.ReadAllLines(path);
            int numSatellites = int.Parse(fileLines[0]);
            NewSatelliteStats[] newSatelliteStats = new NewSatelliteStats[numSatellites];
            for (int i = 0; i < numSatellites; i++)
            {
                string type = fileLines[4*i+1];
                string id = fileLines[4*i+2];
                int elapse_time = int.Parse(fileLines[4 * i + 3]);
                int start_time = int.Parse(fileLines[4 * i + 4]);
                newSatelliteStats[i] = new NewSatelliteStats(type, id, elapse_time, start_time);
            }
            return newSatelliteStats;
        }
        public static Request[] ReadRequests(string path)
        {
            string[] fileLines = System.IO.File.ReadAllLines(path);
            int numRequests = int.Parse(fileLines[0]);
            Request[] newRequests = new Request[numRequests];
            for (int i = 0; i < numRequests; i++)
            {
                char type = fileLines[3 * i + 1][0];
                string location = fileLines[3 * i + 2];
                int time = int.Parse(fileLines[3 * i + 3]);
                newRequests[i] = new Request(type, location, time);
            }
            return newRequests;
        }
    }
}
