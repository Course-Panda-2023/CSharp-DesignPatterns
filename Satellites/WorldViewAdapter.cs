namespace SatellitesDesignPatterns
{
    public class WorldViewAdapter : ISatellite
    {

        private WorldViewSatellite Sat = new();
        private static int id = 0;
        public string value_id { get; }
        private string AdaptedLocation { get; set; }
        public int personal_id { get; }
        public int ElapseTime { get; set; }
        public int StartTime { get; set; }
        public WorldViewAdapter(string val_id, int elapseTime, int startTime) //for reader
        {
            value_id = val_id;
            personal_id = int.Parse(val_id.Substring(3));
            ElapseTime = elapseTime;
            StartTime = startTime;
        }
        public WorldViewAdapter()
        {
            id++;
            Sat = new WorldViewSatellite();
        }

        public void GetData()
        {
            Sat.SampleData();
            string dataAsString = new string(Sat.Data.Select(c => c).ToArray());
            ControlPanelSingleton.GetInstance().MsgToControlUnit(personal_id, string.Join("", dataAsString), AdaptedLocation, GlobalTime.CurrentTime);
        }
        public void SetCommand(string location)
        {
            Sat.SetLocation(location);
            AdaptedLocation = location;
            Sat.SampleData();
        }
    }
}
