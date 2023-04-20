namespace SatellitesDesignPatterns
{
    public static class GlobalTime
    {
        public static int CurrentTime = 1;
    }

    public class Program
    {
        public const string SatellitesPath = @"C:\Users\guyal.DESKTOP-S2VGE0F\Source\Repos\SatellitesDesignPatterns\SatellitesDesignPatterns\data\3S.txt";
        public const string RequestsPath = @"C:\Users\guyal.DESKTOP-S2VGE0F\Source\Repos\SatellitesDesignPatterns\SatellitesDesignPatterns\data\3R.txt";
        static void Main(string[] args)
        {
            ControlPanel c = new ControlPanel();
            c.ReadSatConfigs(SatellitesPath);
            c.ReadReqConfigs(RequestsPath);
            Console.WriteLine("the sim:");
            Thread.Sleep(1000);
            SimulateSatellites(c);
        }

        private static void SimulateSatellites(ControlPanel c)
        {
            Request[] requestsFiltered;
            ISatellite[] satellitesFiltered;
            int SatIndexId = 1;
            do
            {
                requestsFiltered = c.RequestHandler.Where(request => request.OnTime == GlobalTime.CurrentTime).ToArray();
                if (requestsFiltered.Length != 0)
                {
                    foreach (var request in requestsFiltered)
                    {
                        c.SendMessage(request.Data, "weather");
                        c.SendMessage(request.Data, "photo");
                        c.SendMessage(request.Data, "worldview");
                        c.RequestHandler.Remove(request);
                    }
                };
                satellitesFiltered = c.SatList.Where(satellite => c.IsConnectionAvailable(satellite, GlobalTime.CurrentTime)).ToArray();
                if (satellitesFiltered.Length != 0 && c.RequestHandler.Count != 0)
                {
                    foreach (var sat in satellitesFiltered)
                    {
                        Console.Write(SatIndexId);
                        sat.GetData();
                        SatIndexId++;
                    }
                }
                ++GlobalTime.CurrentTime;
                Thread.Sleep(1000);
            } while (true);
        }

        private static void PrintAllSatellites(ControlPanel c)
        {
            int i = 0;
            foreach (ISatellite item in c.SatList)
            {
                Console.WriteLine($"index: {i}, name/id: {item.value_id}, personal id: {item.personal_id}, elapse time: {item.ElapseTime}, start time: {item.StartTime}");
                i++;
            }
        }
    }
}