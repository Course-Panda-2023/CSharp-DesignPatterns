using DPExcersice.FileManager;
using DPExcersice.SingleTon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DPExcersice
{
    public abstract class Satellite : ISatellite
    {
        protected string Id;
        protected string Location;
        protected string Data;
        protected int ElaspsedTime;
        protected int StartTime;
        protected bool InMission;

        public void SetMission(bool missionStatus)
        {
            InMission = missionStatus;
        }

        public string GetData()
        {
            return Data;
        }

        public int GetElapsedTime()
        {
            return ElaspsedTime;
        }

        public int GetStartTime()
        {
            return StartTime;
        }

        public bool IsFittingSatelite(NewActionRequest request)
        {
            int currentTime = SingletonTime.Instance.GetTime();
            bool fittingTime = (currentTime - StartTime) / ElaspsedTime == 0;
            bool answer = (fittingTime && !this.InMission) ?  true:  false;

            return answer;
        }

        public abstract void SampleData();
        public void SetCommand(string location)
        {
            Location = location;
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
    }
}
