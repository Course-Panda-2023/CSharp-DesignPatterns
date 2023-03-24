using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteControl
{
    // SINGLETON DESIGN PATTERN
    class Time : Subject
    {
        private static Time mInstance = null;
        private static int currentTime = 0;
        private Time() { }
        public static Time Instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new Time();
                    currentTime = 0;
                }
                return mInstance; 
            }
        }

        public int CurrentTime
        {
            get
            {
                return currentTime;
            }
        }

        public void Tick()
        {
            currentTime++;
        }
    }
}
