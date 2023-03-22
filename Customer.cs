using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsTamas
{
    internal class Customer
    {
        private Request[] futureRequests;
        public void LoadFutureRequests(string requestPath)
        {
            futureRequests = FileReader.ReadRequests(requestPath);
        }
    
        public void TryToMakeRequests(ControlUnit cu)
        {
            foreach (Request request in futureRequests) 
            {
                if(request.Time == Time.Instance.Seconds) 
                {
                    MakeRequest(cu, request);
                }
            }
        }
        public void MakeRequest(ControlUnit cu, Request request)
        {
            cu.AddRequestToQueue(request);
        }
    }
}
