using DPExcersice.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPExcersice
{
    public interface ISatellite : ObserverDP.Observer
    {
        public string GetData();
        public void SetCommand(string location);

        public void SetMission(bool missionStatus);

        public bool IsFittingSatelite(NewActionRequest request);

        public void SampleData();

    }
}
