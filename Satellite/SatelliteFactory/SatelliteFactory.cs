using Satellite.Satellites;
using System.Runtime.InteropServices;

namespace Satellite.SatelliteFactory
{
    struct Satellite
    {
        public readonly ISatellite satellite;
        public readonly SatelliteTypes type;

        public Satellite(ISatellite satellite, SatelliteTypes type)
        {
            this.satellite = satellite;
            this.type = type;
        }
    }

    internal class SatelliteFactory
    {
        private readonly List<Satellite> satellites = new();

        private Dictionary<SatelliteTypes, Satellite> satelliteCache = new();

        public SatelliteFactory(List<Satellite> satellites)
        {
            this.satellites = satellites;
        }

        public ISatellite GetSatellite(SatelliteTypes type)
        {
            ref var valOrNull = ref CollectionsMarshal.GetValueRefOrAddDefault(satelliteCache, type, out var existed);
            if (existed)
            {
                return satelliteCache[valOrNull.type].satellite;
            }

            ISatellite satellite = satellites.FirstOrDefault(satellite => satellite.type == type).satellite;

            satelliteCache[type] = new Satellite(satellite, type);

            return satellite;

        }
    }
}
