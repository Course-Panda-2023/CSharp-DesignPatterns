using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTamas
{
    public interface ISatellite
    {
        public string Id { get; }
        public int Elapse_time { get; }
        public int Start_time { get; }
        public string? Location { get; }
        public void SetCommend(string location);
        public void SampleData();
        public string GetData(); // also resets for new use
        public bool IsOnMission();
        public string ToString();
        public void TryToSampleData();
    }
}
