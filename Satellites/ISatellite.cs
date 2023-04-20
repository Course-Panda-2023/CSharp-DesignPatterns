namespace SatellitesDesignPatterns
{
    public interface ISatellite
    {

        int StartTime { get; set; }
        int personal_id { get; }
        string value_id { get; }
        int ElapseTime { get; set; }

        void GetData();
        void SetCommand(string location);

    }
}
