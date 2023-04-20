namespace SatellitesDesignPatterns
{
    public class WeatherSatellite : ISatellite
    {
        private string _weatherdata;
        private static int id = 0;

        public string value_id { get; }
        public int personal_id { get; }
        public int ElapseTime { get; set; }
        public int StartTime { get; set; }
        public WeatherSatellite(string val_id, int elapseTime, int startTime) //for reader
        {
            value_id = val_id;
            personal_id = int.Parse(val_id.Substring(2));
            ElapseTime = elapseTime;
            StartTime = startTime;
        }

        public WeatherSatellite() //create on prompt
        {
            id++;
            value_id = $"bw-{id}";
            personal_id = id;
        }

        public void GetData()
        {
            ControlPanelSingleton.GetInstance().MsgToControlUnit(personal_id, SampleData(), _weatherdata, GlobalTime.CurrentTime);
        }

        private string SampleData()
        {
            return _weatherdata.ToUpper();
        }
        public void SetCommand(string location)
        {
            _weatherdata = location;
            SampleData();
        }
    }
}
