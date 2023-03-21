namespace DesignPatterns;

public class SatelliteFactory
{
    public static Satellite Create(string type, string id, int elapseTime, int startTime)
    {
        switch (type)
        {
            case "BW":
                return new SystemPhotoSatellite(id, startTime, elapseTime);
            case "W":
            {
                return new SystemWeatherSatellite(id, startTime, elapseTime);
            }
            case "WV":
                return new WorldViewSatelliteAdapter(id, startTime, elapseTime);
            default:
            {
                return null;
            }
        }
    }
}