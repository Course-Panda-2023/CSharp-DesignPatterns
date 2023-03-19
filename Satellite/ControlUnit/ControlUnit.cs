using Satellite.Exceptions;
using Satellite.Satellites;
using System.Runtime.InteropServices;

namespace Satellite.ControlUnit
{
    internal class ControlUnit
    {
        public List<ISatellite> satelliteObservers = new();

        public ControlUnit() { }

        public ControlUnit(List<ISatellite> satellite) 
        {
            this.satelliteObservers = satellite;
        }

        public void PrintSatelliteData(string satelliteID, string data, string originalData)
        {
            Console.WriteLine($"{satelliteID} returned {data} from {originalData} in time {StageSlotTime.Stage}");
        }

        public void SendDataOfAllSatelliteAboveControlUnit()
        {
            List<ISatellite> filteredSatellites = satelliteObservers
                .Where(satellite => satellite.IsElapsedAboveControlUnit()).Where(satellite => satellite.GetLocation() != null).ToList();

            

            foreach (var satellite in CollectionsMarshal.AsSpan(filteredSatellites))
            {
                satellite.SendDataToControlUnit();
            }
        }

        public void NotifySatellites(SatelliteTypes type, string dataRequestString)
        {
            if (dataRequestString is null) return;

            List<ISatellite>? filteredSatellitesFromCertainType
                = satelliteObservers?.Where(satellite => satellite.FindSatellite(type)).ToList();

            if (filteredSatellitesFromCertainType is null)
            {
                throw new NotFoundSatelliteTypetomExceptions($"Satellite Not Found {type}");
            }

            foreach (var satellite in CollectionsMarshal.AsSpan(filteredSatellitesFromCertainType))
            {
                satellite.SetTargetLocation(dataRequestString);
            }
        }
    }
}
