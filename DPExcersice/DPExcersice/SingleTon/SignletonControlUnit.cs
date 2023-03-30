using DPExcersice;
using DPExcersice.FileManager;
using DPExcersice.ObserverDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPExcersice.SingleTon
{
    public class SignletonControlUnit : ObserverDP.Observer
    {
        private static SignletonControlUnit mInstance = null;
        private List<ISatellite> Satelites = new List<ISatellite>();
        private List<NewActionRequest> PendingRequests = new List<NewActionRequest>();

        private SignletonControlUnit() { }

        public static SignletonControlUnit Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new SignletonControlUnit();
                    SingleTon.SingletonTime.Instance.Attach(mInstance);
                }

                return mInstance;
            }
        }

        public void NewSateliteRequest(NewSateliteRequest newSatelite)
        {
            switch (newSatelite.SateliteType)
            {
                case "BW":
                    Satelites.IDFPhotoSatellite sat1 = new Satelites.IDFPhotoSatellite(newSatelite.Id, newSatelite.ElapseTime, newSatelite.StartTime);
                    Satelites.Add(sat1);
                    break;
                case "W":
                    Satelites.IDFWeatherSatellite sat2 = new Satelites.IDFWeatherSatellite(newSatelite.Id, newSatelite.ElapseTime, newSatelite.StartTime);
                    Satelites.Add(sat2);
                    break;
                case "WV":
                    Satelites.WorldViewSateliteAdapter sat3 = new Satelites.WorldViewSateliteAdapter(newSatelite.Id, newSatelite.ElapseTime, newSatelite.StartTime);
                    Satelites.Add(sat3);
                    break;
            }
        }

        public void RecieveRequest(string requestType, string location)
        {
            PendingRequests.Add(new NewActionRequest(requestType, location));
        }

        public void FitRequestToSatelite(NewActionRequest request)
        {
            foreach (ISatellite satelite in Satelites)
            {
                if (satelite.IsFittingSatelite(request))
                {
                    satelite.SetMission(true);
                    break;
                }
            }
        }

        public void Update()
        {
            //check each request and try to find setalite to get request
            foreach (NewActionRequest request in PendingRequests)
            {
                FitRequestToSatelite(request);
                PendingRequests.Remove(request);
                break;
            }
        }
    }
}
