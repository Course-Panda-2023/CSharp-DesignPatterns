using System.Text;
namespace SatellitesDesignPatterns
{
    public class ControlPanel
    {
        public List<ISatellite> SatList = new List<ISatellite>();
        public SatelliteFactory SatFactory = new SatelliteFactory();
        public List<Request> RequestHandler = new();

        public void CreateSatelliteByRead(string type, string satellite_id, int elapsetime, int starttime)
        {
            SatList.Add(SatFactory.NewSatelliteByRead(type, satellite_id, elapsetime, starttime));
        }
        public void CreateSatellite(string type)
        {
            SatList.Add(SatFactory.NewSatellite(type));
        }

        public void MsgToControlUnit(int satelliteID, string data, string originaldata, int time)
        {
            Console.WriteLine($" returned: {data} from {originaldata} in time {time}");
        }

        public void SendMessage(string message, string typeofmessage)
        {
            if (SatList.Count == 0)
            {
                throw new ArgumentException("List is empty");
            }
            HashSet<string> validMessages = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "weather",
                "photo",
                "worldview"
            };
            if (!validMessages.Contains(typeofmessage))
            {
                throw new ArgumentException($"Invalid message type: {typeofmessage}");
            }
            foreach (var item in SatList)
            {
                item.SetCommand(message);
            }
        }

        public void ReadSatConfigs(string path)
        {
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                string line = reader.ReadLine();
                int lineCount = File.ReadLines(path).Count();
                Console.WriteLine(lineCount); //get line count
                while (!reader.EndOfStream)
                {
                    string satelliteType = reader.ReadLine();
                    string satellite_id = reader.ReadLine();
                    string elapsedTime = reader.ReadLine();
                    string startTime = reader.ReadLine(); //get most of the pattern details
                    switch (satelliteType)
                    { //todo: create satellites
                        case "BW":
                            Console.WriteLine($"Photo satellite created '{satellite_id}' spotted with elapsed time {elapsedTime} and start time {startTime}");
                            CreateSatelliteByRead("photo", satellite_id, int.Parse(elapsedTime), int.Parse(startTime));
                            break;
                        case "WV":
                            Console.WriteLine($"World view satellite created '{satellite_id}' spotted with elapsed time {elapsedTime} and start time {startTime}");
                            CreateSatelliteByRead("worldview", satellite_id, int.Parse(elapsedTime), int.Parse(startTime));
                            //SatList.Add(WorldViewAdapter);
                            break;
                        case "W":
                            Console.WriteLine($"Weather satellite created '{satellite_id}' spotted with elapsed time {elapsedTime} and start time {startTime}");
                            CreateSatelliteByRead("weather", satellite_id, int.Parse(elapsedTime), int.Parse(startTime));
                            //SatList.Add(WeatherSatellite);
                            break;
                        default:
                            //todo: add to read list
                            break;
                    }
                }
                reader.Close();
            }
        }
        public void ReadReqConfigs(string path)
        {
            using (StreamReader reader = new StreamReader(path, Encoding.Default))
            {
                string line = reader.ReadLine();
                int lineCount = File.ReadLines(path).Count();
                Console.WriteLine(lineCount); //get line count
                Request request = new();
                while (!reader.EndOfStream)
                {
                    string requestType = reader.ReadLine();
                    string data = reader.ReadLine();
                    string onTime = reader.ReadLine();

                    switch (requestType)
                    {
                        case "P":
                            Console.WriteLine($"Photo request '{requestType}' made with data {data} on time {onTime}");
                            break;
                        case "W":
                            Console.WriteLine($"Weather request '{requestType}' made with data {data} on time {onTime}");
                            //SatList.Add(WorldViewAdapter);
                            break;
                        default:
                            //todo: add to read list
                            break;
                    }
                    request = new Request(requestType!, data!, Convert.ToInt32(onTime));
                    RequestHandler.Add(request);
                }
                reader.Close();
            }
        }
        public bool IsConnectionAvailable(ISatellite satellite, int currentTime)
        {
            return currentTime % satellite.ElapseTime == satellite.StartTime;
        }
    }
}