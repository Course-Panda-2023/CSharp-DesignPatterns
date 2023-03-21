namespace DesignPatterns;

public class ControlUnit: TimeSubscriber
{
    private readonly List<Satellite> _availableSatellites = new List<Satellite>();
    private readonly List<Satellite> _notAvailableSatellites = new List<Satellite>();
    private readonly List<(RequestType, string)> _requests = new List<(RequestType, string)>();

    public ControlUnit(string filepath)
    {
        string[] lines = System.IO.File.ReadAllLines(filepath);

        for(int i=0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }

            if ((i - 1) % 4 == 3)
            {
                Attach(
                    SatelliteFactory.Create(
                        lines[i-3], 
                        lines[i-2], 
                        Convert.ToInt32(lines[i-1]), 
                        Convert.ToInt32(lines[i])
                    )
                );
            }
        }
    }
    
    public void Attach(Satellite satellite)
    {
        _availableSatellites.Add(satellite);
        Time.Attach(satellite);
    }
    
    public void Remove(Satellite satellite)
    {
        _availableSatellites.Remove(satellite);
        Time.Remove(satellite);
    }

    public void OnTimeStamp(int time)
    {
        // ------------ checking the not available satellites ------------
        
        List<Satellite> satellitesToRemoveFromNotAvailableList = new List<Satellite>();
        
        foreach (Satellite satellite in _notAvailableSatellites)
        {
            if (satellite.StartTime > time)
            {
                // satellite wasn't launched yet
                continue;
            }

            if (IsSatelliteHere(satellite, time) && satellite.WasDataSampled())
            {
                // we can fetch the data from the satellite
                Console.WriteLine($"Satellite {satellite.Id} returned: '{satellite.GetData()}' from '{satellite.GetCommand()}' in time {time}");
                
                _availableSatellites.Add(satellite);
                satellitesToRemoveFromNotAvailableList.Add(satellite);
            }
        }

        foreach (Satellite satellite in satellitesToRemoveFromNotAvailableList)
        {
            _notAvailableSatellites.Remove(satellite);
        }
        
        // ------------ checking if can send one of the new requests ------------

        List<(RequestType, string)> requestsToRemove = new List<(RequestType, string)>();
        
        foreach((RequestType, string) request in _requests)
        {
            Satellite? sat = FindRelevantSatellite(request.Item1, request.Item2, time);

            if (sat == null)
            {
                // there is no available satellite for the request
                continue;
            }
            
            // else: there is an available satellite for our request
            
            _availableSatellites.Remove(sat);
            sat.SetCommand(request.Item2);
            _notAvailableSatellites.Add(sat);
            
            requestsToRemove.Add(request);
        }

        foreach ((RequestType, string) request in requestsToRemove)
        {
            _requests.RemoveAll(t => (t.Item1 == request.Item1) && (t.Item2 == request.Item2));
        }
    }

    public void GetRequest(RequestType requestType, string location)
    {
        _requests.Add((requestType, location));
    }

    private static bool IsSatelliteHere(Satellite satellite, int time)
    {
        return (time - satellite.StartTime) % satellite.ElapseTime == 0;
    }

    private Satellite? FindRelevantSatellite(RequestType requestType, string location, int time)
    {
        Satellite sat = null;
        
        foreach (Satellite satellite in _availableSatellites)
        {
            switch (requestType)
            {
                case RequestType.Photo:
                    if ((satellite is PhotoSatellite) && (IsSatelliteHere(satellite, time)))
                    {
                        sat = satellite;
                        break;
                    }

                    break;
                case RequestType.Weather:
                    if ((satellite is WeatherSatellite) && (IsSatelliteHere(satellite, time)))
                    {
                        sat = satellite;
                        break;
                    }

                    break;
            }
        }

        return sat;
    }
}