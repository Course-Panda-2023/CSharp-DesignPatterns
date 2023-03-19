using Satellite.ControlUnit;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Satellite.Satellites
{
    internal class WorldViewSatelliteAdapter : ISatellite
    {
        private readonly SatelliteTypes type = SatelliteTypes.WorldView;
        private WorldViewSatellite WorldViewSatellite = new();

        public string? satelliteID;
        public uint launchSlotTime { get; set; }

        public uint elapsetime { get; set; }

        public bool FindSatellite(SatelliteTypes type)
        {
            return this.type == type;
        }

        public bool IsElapsedAboveControlUnit()
        {
            uint relativeStage = StageSlotTime.Stage % elapsetime;
            return relativeStage == launchSlotTime;
        }

        public void SendDataToControlUnit()
        {
            WorldViewSatellite.SampleData();
            string data = String.Join("", 
                this.WorldViewSatellite.Data.Select(c => (c>='a'? (char)(c - 'a' + 'A') : (char)(c+'a'-'A')).ToString()));
           
            ControlUnitSingleton.GetInstance().PrintSatelliteData(this.satelliteID!, data, this.WorldViewSatellite.Location);
            this.WorldViewSatellite.Location = null;
        }

        public void SetElapsedTime(string elapsedTime)
        {
            this.elapsetime = Convert.ToUInt32(elapsedTime);
        }

        public uint GetLaunchTime()
        {
            return this.launchSlotTime;
        }

        public string? GetLocation()
        {
            return this.WorldViewSatellite.Location;
        }

        public void SetID(string ID)
        {
            this.satelliteID = ID;
        }

        public void SetLaunchTime(string launchTime)
        {
            this.launchSlotTime = Convert.ToUInt32(launchTime);
        }

        public void SetTargetLocation(string location)
        {
            WorldViewSatellite.SetLocation(location);
        }

    }
}
