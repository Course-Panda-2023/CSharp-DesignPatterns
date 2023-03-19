using Satellite.ControlUnit;
using Satellite.SatelliteFactory;
using Satellite.Satellites;
using System.Collections;
using System.Collections.Specialized;

string[] lines = System.IO.File.ReadAllLines(@"C:\Users\AssafHillel\source\repos\Satellite\Satellite\data\3S.txt");


uint numberOfSatellites = Convert.ToUInt16(lines[0]);

string[] RemoveFirst(string[] lines, out string[] satelliteConfiguration)
{
    satelliteConfiguration = new string[lines.Length - 1];

    lines.AsSpan(1, lines.Length - 1).CopyTo(satelliteConfiguration);

    return satelliteConfiguration;
}
string[] satelliteConfiguration;

RemoveFirst(lines, out satelliteConfiguration);


WeatherSatellite weatherSatellite = new()
{
    Type = SatelliteTypes.Weather,
    launchSlotTime = 2,
    elapsetime = 4
};

WorldViewSatelliteAdapter adapter = new()
{
    elapsetime = 3,
    launchSlotTime = 2
};

PhotoSatellite photoSatellite = new()
{
    Type = SatelliteTypes.BlueWhite,
    launchSlotTime = 1,
    elapsetime = 2
};



List<Satellite.SatelliteFactory.Satellite> satellites = new()
{
    new Satellite.SatelliteFactory.Satellite(weatherSatellite, SatelliteTypes.Weather),
    new Satellite.SatelliteFactory.Satellite(photoSatellite, SatelliteTypes.BlueWhite),
    new Satellite.SatelliteFactory.Satellite(adapter, SatelliteTypes.WorldView)
};

SatelliteFactory satelliteFactory = new SatelliteFactory(satellites);

Dictionary<string, SatelliteTypes> collection = new Dictionary<string, SatelliteTypes>(StringComparer.OrdinalIgnoreCase)
{
    { "BW", SatelliteTypes.BlueWhite },
    { "W", SatelliteTypes.Weather },
    { "WV", SatelliteTypes.WorldView }
};

List<ISatellite> fromFileSatellite = new();
for (uint index = 0; index < numberOfSatellites * 4; index += 4)
{
    SatelliteTypes type = collection[satelliteConfiguration[index]];
    ISatellite satellite = satelliteFactory.GetSatellite(type);
    satellite.SetID(satelliteConfiguration[index + 1]);
    satellite.SetElapsedTime(satelliteConfiguration[index + 2]);
    satellite.SetLaunchTime(satelliteConfiguration[index + 3]);
    fromFileSatellite.Add(satellite);
}


ISatellite satelliteWeather = satelliteFactory.GetSatellite(SatelliteTypes.BlueWhite);
ISatellite satellitePicture = satelliteFactory.GetSatellite(SatelliteTypes.Weather);

List<ISatellite> satellites1 = new List<ISatellite>()
{
   weatherSatellite,
   adapter,
   photoSatellite
};

ControlUnit controlUnit = new ControlUnit(fromFileSatellite);

string[] linesOfRequestFile = System.IO.File.ReadAllLines(@"C:\Users\AssafHillel\source\repos\Satellite\Satellite\data\3R.txt");

uint numberOfRequests = Convert.ToUInt16(linesOfRequestFile[0]);

string[] requestsLine;
RemoveFirst(linesOfRequestFile, out requestsLine);

NameValueCollection collection1 = new()
{
    { "P", "Blue White" },
    { "P", "World View" },
    { "W", "Weather" }
};

Dictionary<string, SatelliteTypes> dictionary = new Dictionary<string, SatelliteTypes>(StringComparer.OrdinalIgnoreCase)
{
    { "Blue White", SatelliteTypes.BlueWhite },
    { "World View", SatelliteTypes.WorldView },
    { "Weather", SatelliteTypes.Weather }
};


Dictionary<uint, packet> requests = new Dictionary<uint, packet>();
Dictionary<uint, packet> requests2 = new Dictionary<uint, packet>();

for (uint index = 0; index < numberOfRequests * 3; index += 3)
{ 
    string satellite = collection1.Get(requestsLine[index]);

    SatelliteTypes result;
    if (!dictionary.ContainsKey(satellite))
    {
        packet packetOfRequest1 = new();
        packet packetOfRequest2 = new();
        result = dictionary[satellite.Split(",")[0]!];

        SatelliteTypes result1 = dictionary[satellite.Split(",")[1]!];
        packetOfRequest2.type = result1;
        packetOfRequest1.type = result;
        packetOfRequest1.data = packetOfRequest2.data = requestsLine[index + 1];
        requests[Convert.ToUInt16(requestsLine[index + 2])] = packetOfRequest1;
        requests2[Convert.ToUInt16(requestsLine[index + 2])] = packetOfRequest2;
        continue;
    }
    result = dictionary[satellite!];

    packet packetOfRequest = new();
    packetOfRequest.type = result;
    packetOfRequest.data = requestsLine[index + 1];
    requests[Convert.ToUInt16(requestsLine[index + 2])] = packetOfRequest;
}


while (true)
{
    if (StageSlotTime.Stage == UInt32.MaxValue) StageSlotTime.Stage = 0;
    
    packet val, val2;
    if (requests.TryGetValue(StageSlotTime.Stage, out val))
    {
        controlUnit.NotifySatellites(val.type, val.data);
        requests.Remove(StageSlotTime.Stage);
    }

    if (requests2.TryGetValue(StageSlotTime.Stage, out val2))
    {
        controlUnit.NotifySatellites(val2.type, val2.data);
        requests2.Remove(StageSlotTime.Stage);
    }

    controlUnit.SendDataOfAllSatelliteAboveControlUnit();

    

    ++StageSlotTime.Stage;
    Thread.Sleep(1000);
}