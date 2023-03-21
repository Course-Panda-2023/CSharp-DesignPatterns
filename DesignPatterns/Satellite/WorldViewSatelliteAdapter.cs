using SatelliteControl;

namespace DesignPatterns;

public class WorldViewSatelliteAdapter: PhotoSatellite
{
    private readonly WorldViewSatellite _worldViewSatellite;
    private string _location;
    private string _data;
    private bool _wasDataSampled = false;
    
    public WorldViewSatelliteAdapter(string id, int startTime, int elapseTime): base(id, startTime, elapseTime)
    {
        _worldViewSatellite = new WorldViewSatellite();
    }
    
    public override void SetCommand(string location)
    {
        _location = location;
        if (_worldViewSatellite.SetLocation(location) == false)
        {
            _worldViewSatellite.View();
            _worldViewSatellite.SetLocation(location);
        }

        _wasDataSampled = false;
    }

    protected override void SampleData()
    {
        _worldViewSatellite.SampleData();
        
        int[] data = _worldViewSatellite.View();
        int[] reconstructedData = data.Select((i) => i >= 'a' + 'a' - 'A'? i - ('a' - 'A') : i).ToArray();
        char[] chars = new char[reconstructedData.Length];

        for (int i = 0; i < reconstructedData.Length; i++)
        {
            chars[i] = (char) reconstructedData[i];
        }

        String reconstructedLocation = new String(chars);
        _data = reconstructedLocation.ToLower();
        
        _wasDataSampled = true;
    }
    
    public override string GetData()
    {
        return _data;
    }
    
    public override string GetCommand()
    {
        return _location;
    }

    public override bool WasDataSampled()
    {
        return _wasDataSampled;
    }
}