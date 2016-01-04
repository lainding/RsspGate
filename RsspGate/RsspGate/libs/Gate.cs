using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs
{
    abstract class Gate
    {
        public string Name
        {
            get; set;
        }
        protected List<Route> routes = new List<Route>();
        public List<Route> Routes
        {
            get
            {
                return routes;
            }
        }
        public void AddRoute(Route route)
        {
            routes.Add(route);
        }
        public abstract Gate Start();
        public abstract Gate Stop();
        public event EventHandler<DatagramReceivedEventArgs<byte[]>> DatagramReceived;
        protected void RaiseDatagramReceived(Gate sender, byte[] datagram, IPEndPoint endpoint)
        {
            if (DatagramReceived != null)
            {
                DatagramReceived(sender, new DatagramReceivedEventArgs<byte[]>(datagram, endpoint));
            }
        }

        public abstract void Send(Device device, byte[] data);
    }
}
