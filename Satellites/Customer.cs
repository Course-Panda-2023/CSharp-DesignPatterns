using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteControl
{
    public class Customer : Observer
    {
        private Time time;
        private List<Request> allRequests;
        private const int NumOfLinesPerRequest = 3;
        public Customer(string rInfo) 
        {
            time = Time.Instance;
            allRequests = new List<Request>();
            ConvertRequestInfoToList(rInfo);
            time.Attach(this);
        }

        public void ConvertRequestInfoToList(string rInfo)
        {
            string[] lines = System.IO.File.ReadAllLines(rInfo);
            string[] currentRequest = new string[NumOfLinesPerRequest];
            int currRequestLineNum;
            for (int i = 1; i < lines.Length; i++)
            {
                currRequestLineNum = (i - 1) % NumOfLinesPerRequest;
                AddRequestToList(currentRequest, currRequestLineNum, i);
                currentRequest[currRequestLineNum] = lines[i];
            }
            currRequestLineNum = (lines.Length - 1) % NumOfLinesPerRequest;
            AddRequestToList(currentRequest, currRequestLineNum, lines.Length);
        }

        public void AddRequestToList(string[] currentRequest, int currRequestLineNum, int index)
        {
            if (currRequestLineNum == 0 && index > NumOfLinesPerRequest)
            {
                Request tempRequest = new Request(currentRequest[0], currentRequest[1], Convert.ToInt32(currentRequest[2]));
                allRequests.Add(tempRequest);
            }
        }

        public List<Request> MakeRequest()
        {
            // Sends the requests to the control unit.
            List<Request> sendRequests = new List<Request>();
            foreach (Request currentRequest in allRequests)
            {
                if(currentRequest.OnTime == Update())
                {
                    sendRequests.Add(currentRequest);
                }
            }
            RemoveSentRequests(sendRequests);
            return sendRequests;
        }

        public void RemoveSentRequests(List<Request> sentRequest)
        {
            foreach(Request r in sentRequest)
            {
                if(allRequests.Contains(r))
                    allRequests.Remove(r);
            }
        }

        public int GetNumOfAllRequests()
        {
            return allRequests.Count;
        }

        public int Update()
        {
            Time time = Time.Instance;
            return time.CurrentTime;
        }
    }
}
