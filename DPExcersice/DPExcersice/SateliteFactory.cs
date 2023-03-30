using DPExcersice.Satelites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPExcersice
{
    static class SateliteFactory
    {
        public static ISatellite CreateSatelite(SateliteType sataliteType, string id, int elapsTime, int startTime)
        {
            switch (sataliteType)
            {
                case SateliteType.IDFWeather:
                    return new IDFWeatherSatellite(id, elapsTime, startTime);
                case SateliteType.IDFPhoto:
                    return new IDFPhotoSatellite(id, elapsTime, startTime);
                case SateliteType.WorldViewPhoto:
                    return new WorldViewSateliteAdapter(id, elapsTime, startTime);

            }
            return null;
        }
    }
}
