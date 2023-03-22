using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SatelliteControl;

namespace DesignPatternsTamas
{
    public class WorldViewAdapter : ISatellite
    {
        private WorldViewSatellite adaptee;
        public WorldViewAdapter(string id, int elapse_time, int start_time)
        {
            this.Id = id;
            this.Elapse_time = elapse_time;
            this.Start_time = start_time;
            this.adaptee = new WorldViewSatellite();
        }
        
        public string Id { get; }
        public int Elapse_time { get; }
        public int Start_time { get; }
        public string? Location { get; private set; }
        private int? sampleTime;

        public string GetData()
        {
            this.Location = null;
            this.sampleTime = null;
            string data = "";
            int[] almostData = this.adaptee.View();
            foreach (int num in almostData)
            {
                data += TurnNumToCorrectChar(num);
            }
            return data;
        }
        public static char TurnNumToCorrectChar(int num)
        {
            if (129 <= num && num <= 154) { return (char)(num - 32); } //lowercase
            else if (65 <= num && num <= 90) { return (char)(num + 32); } //uppercase
            else throw new ArgumentException();
        }
        public bool IsOnMission()
        {
            return this.Location != null;
        }
        public void SampleData()
        {
            this.adaptee.SampleData();
            Console.WriteLine("sampled data: " + this.Location + " to " + string.Concat(Enumerable.Repeat("?", this.Location.Length)));
        }
        public void TryToSampleData()
        {
            if (Time.Instance.Seconds == this.sampleTime) { this.SampleData(); }
        }
        public void SetCommend(string location)
        {
            this.Location = location;
            this.adaptee.SetLocation(location);
            this.sampleTime = Time.Instance.Seconds + this.Elapse_time / 2;
        }
        public override string ToString()
        {
            return $"WorldViewAdapter(Id={Id}, Elapse_time={Elapse_time}, Start_time={Start_time}, Location={Location})";
        }
    }
}
