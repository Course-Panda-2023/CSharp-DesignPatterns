using SatelliteControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteControl
{
    abstract class Satellite : ISatellite, Observer
    {
        protected int elapsedTime;
        protected int startTime;
        protected string id;
        protected string type;
        protected Time time;
        protected List<Request> requests;

        public int ElapsedTime { get { return elapsedTime; } }
        public int StartTime { get { return startTime; } }
        public string Type { get { return type; } }
        public string ID { get { return id; } }
        public int RequestAmount { get { return requests.Count; } }

        public Satellite(string[] sInfo)
        {
            time = Time.Instance;
            elapsedTime = Convert.ToInt32(sInfo[2]);
            startTime = Convert.ToInt32(sInfo[3]);
            id = sInfo[1];
            type = sInfo[0];
            requests = new List<Request>();
        }

        public void SetCommand(Request location)
        {
            // Adds the request to a list
            requests.Add(location);
        }

        public int GetNextTime(int currentTime)
        {
            // Gets the next time the satellite reaches the control unit.
            int tempTime = currentTime;
            while ((tempTime - startTime) % elapsedTime != 0)
            {
                tempTime++;
            }
            return tempTime;
        }

        public int Update()
        {
            // Returns the point of the lap of current satellite.
            Time time = Time.Instance;
            return (time.CurrentTime - StartTime) % ElapsedTime;
        }

        public virtual List<Request> GetData()
        {
            // Returns the processed requests to the control unit.
            List<Request> tempRequests = new List<Request>();
            if (Update() == 0)
            {
                foreach (Request r in requests)
                {
                    tempRequests.Add(r);
                }
                requests.Clear();
            }
            return tempRequests;
        }

        public abstract void SampleData();
    }
}
