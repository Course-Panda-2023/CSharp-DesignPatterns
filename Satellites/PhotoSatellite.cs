namespace SatellitesDesignPatterns
{
    public class PhotoSatellite : ISatellite
    {
        public string photodata { get; set; }
        private static int id = 0;
        public string value_id { get; }
        public int personal_id { get; }
        public int ElapseTime { get; set; }
        public int StartTime { get; set; }
        public PhotoSatellite(string val_id, int elapseTime, int startTime) //for reader
        {
            value_id = val_id;
            personal_id = int.Parse(val_id.Substring(3));
            ElapseTime = elapseTime;
            StartTime = startTime;
        }
        public PhotoSatellite() //create on prompt
        {
            id++;
            value_id = $"bw-{id}";
            personal_id = id;
        }
        public void GetData()
        {
            ControlPanelSingleton.GetInstance().MsgToControlUnit(personal_id, SampleData(), photodata, GlobalTime.CurrentTime);
        }
        private string SampleData()
        {
            return photodata.ToLower();
        }
        public void SetCommand(string location)
        {
            photodata = location;
            SampleData();
        }
    }
}
