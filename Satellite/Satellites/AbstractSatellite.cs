using Satellite.ControlUnit;
using System.Diagnostics;

namespace Satellite.Satellites
{
    internal abstract class AbstractSatellite : ISatellite
    {
        public string? SatelliteID { get; set; }
        public uint launchSlotTime { get; set; }

        public uint elapsetime { get; set; }
        protected string? Location { get; set; }

        protected Queue<string> LocationsQueue = new();

        private string? _data;

        protected string Data(string _data) => this._data = _data;

        public SatelliteTypes Type { get; init; }
        public void SendDataToControlUnit()
        {
            ControlUnitSingleton.GetInstance().PrintSatelliteData(SatelliteID!, _data!, Location!);

            // After location is sent then for next time location is null
            Location = null;

            while (LocationsQueue.Count > 0)
            {
                string location = LocationsQueue.Dequeue();
                Location = location;
                SampleData();
                ControlUnitSingleton.GetInstance().PrintSatelliteData(SatelliteID!, _data!, location!);
                Location = null;
            }
        }

        public string? GetLocation()
        {
            return Location;
        }

        public abstract void SetTargetLocation(string location);

        public bool FindSatellite(SatelliteTypes type)
        {
            return this.Type == type;
        }

        public bool IsElapsedAboveControlUnit()
        {
            if (this.launchSlotTime == StageSlotTime.Stage)
            {
                return false;
            }
            
            uint relativeStage = StageSlotTime.Stage % elapsetime;
            return relativeStage == launchSlotTime;
        }

        public void SetElapsedTime(string elapsedTime)
        {
            this.elapsetime = Convert.ToUInt32(elapsedTime);
        }

        public void SetLaunchTime(string launchTime)
        {
            this.launchSlotTime = Convert.ToUInt32(launchTime);
        }

        public void SetID(string ID)
        {
            this.SatelliteID = ID;
        }

        public uint GetLaunchTime()
        {
            return this.launchSlotTime;
        }

        protected abstract void SampleData();
    }
}
