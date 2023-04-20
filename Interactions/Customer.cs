namespace SatellitesDesignPatterns
{
    public class Customer
    {
        private string Name { get; set; }
        private int Time { get; set; }

        public Customer(string name, int time)
        {
            Name = name;
            Time = time;
        }
        public void SendData(string type, string data, ControlPanel c)
        {
            c.SendMessage(type, data);
        }
    }
}

