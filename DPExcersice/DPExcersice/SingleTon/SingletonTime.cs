using DPExcersice.ObserverDP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPExcersice.SingleTon
{
    public class SingletonTime : Subject
    {
        private static SingletonTime mInstance = null;
        private int Time = 0;

        public void Tick()
        {
            Time++;
        }
        public int GetTime()
        {
            return this.Time;
        }

        private SingletonTime() { }

        public static SingletonTime Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new SingletonTime();
                }

                return mInstance;
            }
        }
    }
}
