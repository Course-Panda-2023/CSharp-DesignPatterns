using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SatelliteControl;

namespace DesignPatternsTamas
{
    public class ControlUnit
    {
        private ISatellite[] satellites;
        private List<Request> requests;
        public ControlUnit()
        {
            this.requests = new List<Request>();
        }

        public void LoadSatelliteFromFile(string path)
        {
            NewSatelliteStats[] readSatelliteStats = FileReader.ReadSatelliteStats(path);
            this.satellites = new ISatellite[readSatelliteStats.Length];
            for (int i = 0; i < readSatelliteStats.Length; i++)
            {
                this.satellites[i] = SatelliteFactory.Create(readSatelliteStats[i]);
            }
        }
        public void AddRequestToQueue(Request request)
        {
            Console.WriteLine("Added to ControlUnit " + request.ToString());
            requests.Add(request); 
        }
        public bool AttachRequest(Request request) 
        {
            foreach (ISatellite satellite in satellites)
            {
                if (!satellite.IsOnMission() && Time.Instance.Seconds % satellite.Elapse_time == satellite.Start_time)
                {
                    if (request.Type == 'W' && satellite is WeatherSatellite)
                    {
                        satellite.SetCommend(request.Location);
                        //requests.Remove(request);
                        Console.WriteLine($"on time {Time.Instance.Seconds} Sent {request} to {satellite.ToString()}");
                        return true;
                    }
                    if (request.Type == 'P' && (satellite is PhotoSatellite || satellite is WorldViewAdapter))
                    {
                        satellite.SetCommend(request.Location);
                        //requests.Remove(request);
                        Console.WriteLine($"on time {Time.Instance.Seconds} Sent {request} to {satellite.ToString()}");
                        return true;
                    }
                }
            }
            if(!(request.Type == 'W' || request.Type == 'P')) { throw new Exception(); }
            return false;
        }
        public void GetDataFromSatellites()
        {
            foreach (ISatellite satellite in this.satellites)
            {
                if ((Time.Instance.Seconds % satellite.Elapse_time == satellite.Start_time) && satellite.IsOnMission())
                {
                    string satId = satellite.Id;
                    string satLocation = satellite.Location;
                    string data = satellite.GetData();
                    ControlUnit.PrintData(satId, data, satLocation, Time.Instance.Seconds);
                }
            }
        }
        public void TryAttachRequests()
        {
            List<Request>  thisRequestCopy = new List<Request>(this.requests);
            foreach (Request request in thisRequestCopy)
            {
                if (!request.IsAssigned && Time.Instance.Seconds >= request.Time)
                {
                    request.IsAssigned = AttachRequest(request);
                    if (request.IsAssigned) { this.requests.Remove(request);  }
                    
                }
            }
        }
        public void MakeSatellitesTryToSampleData()
        {
            foreach (ISatellite satellite in this.satellites)
            {
                if (satellite.IsOnMission()) { satellite.TryToSampleData(); }
            }
        }
        public static void PrintData(string satelliteId, string data, string original_data, int time)
        {
            Console.WriteLine($"{satelliteId} returned: '{data}' from '{original_data}' in time {time}");
        }
        public int NumRequests()
        {
            return this.requests.Count;
        }
    }
}
