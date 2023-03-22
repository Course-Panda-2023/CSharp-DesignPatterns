using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTamas
{
    public abstract class IsraeliSatellite : ISatellite
    {
        public string Id { get; }
        public int Elapse_time { get; }
        public int Start_time { get; }
        public string? Location { get; private set; }
        protected string? data;
        private int? sampleTime;

        public IsraeliSatellite(string id, int elapse_time, int start_time)
        {
            this.Id = id;
            this.Elapse_time = elapse_time;
            this.Start_time = start_time;
      
        }
        public void SetCommend(string location)
        {
            this.Location = location;
            this.sampleTime = Time.Instance.Seconds + this.Elapse_time / 2;
        }
        public string GetData()
        {
            string Data = this.data;
            this.Location = null;
            this.data = null;
            this.sampleTime = null;
            return Data;
        }
        public bool IsOnMission()
        {
            return this.Location != null;
        }
        public abstract void SampleData();
        public abstract override string ToString();

        public void TryToSampleData()
        {
            if(Time.Instance.Seconds == this.sampleTime) { this.SampleData(); }
        }
    }
}
