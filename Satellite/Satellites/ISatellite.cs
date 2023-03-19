namespace Satellite.Satellites
{
    struct packet
    {
        public string data;
        public SatelliteTypes type;
    };

    internal interface ISatellite
    {
        void SendDataToControlUnit();

        void SetTargetLocation(string location);

        bool FindSatellite(SatelliteTypes type);

        bool IsElapsedAboveControlUnit();

        void SetElapsedTime(string elapsedTime);

        void SetLaunchTime(string launchTime);

        void SetID(string ID);

        string? GetLocation();

        uint GetLaunchTime();
    }
}
