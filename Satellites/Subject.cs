using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteControl
{
    abstract class Subject
    {
        private List<Observer> mObservers = new List<Observer>();
        public void Attach(Observer observer)
        {
            mObservers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            mObservers.Remove(observer);
        }

        public void Notify()
        {
            foreach (Observer observer in mObservers)
            {
                observer.Update();
            }
        }
    }
}
