namespace SatellitesDesignPatterns
{
    public class SatelliteFactory
    {
        public ISatellite NewSatelliteByRead(string type, string satellite_id, int elapsedTime, int startTime)
        {
            switch (type.ToLower())
            {
                case "weather":
                    return new WeatherSatellite(satellite_id, elapsedTime, startTime);
                case "photo":
                    return new PhotoSatellite(satellite_id, elapsedTime, startTime);
                case "worldview":
                    return new WorldViewAdapter(satellite_id, elapsedTime, startTime);
                default:
                    throw new ArgumentException($"Invalid shape type: {type}");
            }
        }
        public ISatellite NewSatellite(string type)
        {
            switch (type.ToLower())
            {
                case "weather":
                    return new WeatherSatellite();
                case "photo":
                    return new PhotoSatellite();
                case "worldview":
                    return new WorldViewAdapter();
                default:
                    throw new ArgumentException($"Invalid shape type: {type}");
            }
        }
    }
}