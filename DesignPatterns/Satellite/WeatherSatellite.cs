namespace DesignPatterns;

public abstract class WeatherSatellite: Satellite
{
    protected WeatherSatellite(string id, int startTime, int elapseTime) : base(id, startTime, elapseTime)
    {
        
    }
}