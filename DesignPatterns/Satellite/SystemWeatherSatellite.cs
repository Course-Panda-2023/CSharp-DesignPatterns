namespace DesignPatterns;

public class SystemWeatherSatellite: WeatherSatellite
{
    private string _location;
    private string _data;
    private bool _wasDataSampled = false;

    public SystemWeatherSatellite(string id, int startTime, int elapseTime) : base(id, startTime, elapseTime)
    {
        _location = null;
        _data = null;
    }

    public override void SetCommand(string location)
    {
        _location = location;
        _wasDataSampled = false;
    }

    protected override void SampleData()
    {
        _data = _location.ToUpper();
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