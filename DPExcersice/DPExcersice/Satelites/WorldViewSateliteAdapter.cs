using DPExcersice.FileManager;
using DPExcersice.SingleTon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPExcersice.Satelites
{
    public class WorldViewSateliteAdapter : ISatellite
    {
        private WorldViewSatellite mAdaptee;
        private string Id;
        private int ElaspsedTime;
        private int StartTime;
        private bool InMission;

        public void SetMission(bool missionStatus)
        {
            InMission = missionStatus;
        }

        public WorldViewSateliteAdapter(string id, int elaspsedTime, int startTime)
        {
            mAdaptee = new WorldViewSatellite();
            Id = id;
            ElaspsedTime = elaspsedTime;
            StartTime = startTime;
            SingleTon.SingletonTime.Instance.Attach(this);
        }

        public WorldViewSateliteAdapter()
        {
            SingleTon.SingletonTime.Instance.Attach(this);
        }

        public string GetData()
        {
            return new string(Array.ConvertAll(mAdaptee.View(), x => (char)x));
        }

        public void SetCommand(string location)
        {
            mAdaptee.SetLocation(location);
        }

        public void SampleData()
        {
            mAdaptee.SampleData();
        }

        public void Update()
        {
            bool isTimeGoodToAction = (SingletonTime.Instance.GetTime() - this.StartTime) % this.ElaspsedTime == ElaspsedTime / 2;
            if (isTimeGoodToAction)
            {
                SampleData();
                Console.WriteLine(GetData());
                SetMission(false);
            }
        }

        public bool IsFittingSatelite(NewActionRequest request)
        {
            int currentTime = SingletonTime.Instance.GetTime();
            bool fittingTime = (currentTime - StartTime) / ElaspsedTime == 0;
            bool answer = (fittingTime && !this.InMission) ? true : false;

            return answer;
        }
    }
}
