using DPExcersice.FileManager;
using DPExcersice.SingleTon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPExcersice
{
    public class Customer : ObserverDP.Observer
    {
        private List<NewActionRequest> Requests = new List<NewActionRequest>();

        public Customer(string filePath)
        {
            Requests = FileReader.ReadNewRequest(filePath);
            SingleTon.SingletonTime.Instance.Attach(this);
        }

        public void MakeRequest(string requestType, string location)
        {
            SignletonControlUnit.Instance.RecieveRequest(requestType, location);
        }

        private void AddNewRequest(NewActionRequest newAction)
        {
            Requests.Add(newAction);
        }

        private void SendRequestAtTime()
        {
            foreach (NewActionRequest request in Requests) 
            {
                if (SingletonTime.Instance.GetTime() == request.OnTime)
                {
                    MakeRequest(request.SateliteType, request.Location);
                }
            }
        }

        public void Update()
        {
            this.SendRequestAtTime();
        }
    }
}
