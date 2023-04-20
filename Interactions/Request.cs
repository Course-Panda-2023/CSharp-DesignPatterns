namespace SatellitesDesignPatterns
{
    public struct Request
    {
        public string Type { get; set; }
        public string Data { get; set; }
        public int OnTime { get; set; }
        public Request(string type, string data, int onTime)
        {
            Type = type;
            Data = data;
            OnTime = onTime;
        }

    }
}
