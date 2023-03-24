using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteControl
{
    class ControlUnit : Observer
    {
        const int NumOfLinesPerSatellite = 4;

        private Time time;
        private Customer customer;
        private List<Satellite> satellites;
        private enum satellite
        {
            W,
            BW,
            WV
        }

        public ControlUnit(Customer c, string satelliteInfo)
        {
            time = Time.Instance;
            customer = c;
            satellites = new List<Satellite>();
            ConvertSatelliteInfoToList(satelliteInfo);
            time.Attach(this);
        }

        public void ConvertSatelliteInfoToList(string satelliteInfo)
        {
            string[] lines = System.IO.File.ReadAllLines(satelliteInfo);
            string[] currentSatellite = new string[NumOfLinesPerSatellite];
            int currSatelliteLineNum;
            for (int i = 1; i < lines.Length; i++)
            {
                currSatelliteLineNum = (i - 1) % NumOfLinesPerSatellite;
                AddSatelliteToList(currentSatellite, currSatelliteLineNum, i);
                currentSatellite[currSatelliteLineNum] = lines[i];
            }
            currSatelliteLineNum = (lines.Length - 1) % NumOfLinesPerSatellite;
            AddSatelliteToList(currentSatellite, currSatelliteLineNum, lines.Length);
        }

        public void AddSatelliteToList(string[] currentSatellite, int currSatelliteLineNum, int index)
        {
            if (currSatelliteLineNum == 0 && index > NumOfLinesPerSatellite)
            {
                string satelliteType = currentSatellite[0];
                Satellite s = SatelliteFactory.Create(satelliteType, currentSatellite);
                time.Attach(s);
                satellites.Add(s);
            }
        }

        public void SatelliteCommunication()
        {
            // Sends the requests to the correct satellite at the correct time,
            // and works until all the satellites return their data.
            List<Request> receivedRequests = new List<Request>();
            List<Request> processedRequests = new List<Request>();
            bool IsDone = false;
            int numOfRequests = customer.GetNumOfAllRequests();

            while (customer.GetNumOfAllRequests() > 0 || processedRequests.Count != numOfRequests || !IsDone)
            {
                time.Notify();
                IsDone = GetDataFromSatellite();
                SatelliteSampling();
                List<Request> sentRequests = customer.MakeRequest();
                AddRequestsToReceivedRequests(sentRequests, ref receivedRequests);
                foreach (Request r in receivedRequests)
                {
                    if(SendCommandsToSatellite(r))
                    {
                        processedRequests.Add(r);
                        IsDone = false;
                    }
                }
                RemoveRequests(ref processedRequests, ref receivedRequests);
                time.Tick();
            }
        }

        public void PrintData(string id, string data, string originalData, int time)
        {
            Console.WriteLine($"{id} returned: {data} from {originalData} in time {time}");
        }

        public bool SendCommandsToSatellite(Request r)
        {
            // Sends the requests to the correct satellite at the correct time.
            Satellite s = GetSatellite(r.Type);
            if (s.Update() == 0)
            {
                s.SetCommand(r);
                return true;
            }
            return false;
        }

        public bool GetDataFromSatellite()
        {
            // Gets the sampled data from the satellite.
            bool IsDone = true;
            foreach (Satellite s in satellites)
            {
                if (s.Update() == 0)
                {
                    List<Request> requests = s.GetData();
                    foreach (Request r in requests)
                    {
                        PrintData(s.ID, r.ProcessedData, r.Data, time.CurrentTime);
                    }
                }
                if(s.RequestAmount > 0)
                {
                    IsDone = false;
                }
            }
            return IsDone;
        }

        public Satellite GetSatellite(string request)
        {
            // Gets the right satellite for current request.
            Satellite s = null;
            foreach (Satellite s1 in satellites)
                if(TransferData(request) == s1.Type)
                    s = s1;
            return s;
        }

        public string TransferData(string s)
        {
            // Changes the string to be the satellite type instead of the data type.
            if (s == satellite.W.ToString())
                return s;
            else if (BWSatelliteIsCloser())
                return satellite.BW.ToString();
            return satellite.WV.ToString();
        }

        public bool BWSatelliteIsCloser()
        {
            // Checks if the PhotoSatellite is closer than the WorldViewSatellite to the control unit.
            int tempCurrentTimeBW = 0;
            int tempCurrentTimeWV = 0;
            foreach (Satellite s in satellites)
            {
                if (s.Type == satellite.BW.ToString())
                    tempCurrentTimeBW = s.GetNextTime(Update());
                else if (s.Type == satellite.WV.ToString())
                    tempCurrentTimeWV = s.GetNextTime(Update());
            }
            return tempCurrentTimeBW <= tempCurrentTimeWV;
        }

        public void AddRequestsToReceivedRequests(List<Request> sentRequests, ref List<Request> receivedRequests)
        {
            foreach(Request r in sentRequests)
            {
                if (r != null)
                {
                    receivedRequests.Add(r);
                }
            }
        }

        public void RemoveRequests(ref List<Request> processedRequests, ref List<Request> receivedRequests)
        {
            foreach (Request r in processedRequests)
            {
                if(receivedRequests.Contains(r))
                    receivedRequests.Remove(r);
            }
        }

        public void SatelliteSampling()
        {
            foreach(Satellite s in satellites)
            {
                if (s.Update() == s.ElapsedTime / 2)
                {
                    s.SampleData();
                }
            }
        }

        public int Update()
        {
            Time time = Time.Instance;
            return time.CurrentTime;
        }
    }
}
