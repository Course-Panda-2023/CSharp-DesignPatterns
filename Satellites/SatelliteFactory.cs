using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SatelliteControl
{
    // FACTORY DESIGN PATTERN
    static class SatelliteFactory
    {
        public static Satellite Create(string type, string[] sInfo)
        {
            switch (type)
            {
                case "WV":
                    {
                        return new WVAdapter(sInfo);
                    }
                case "BW":
                    {
                        return new PhotoSatellite(sInfo);
                    }
                case "W":
                    {
                        return new WeatherSatellite(sInfo);
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
