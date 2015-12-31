using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RsspGate.libs;

namespace RsspGate
{
    class InterfaceProcess
    {
        public static void ProcessUDPInput(object sender, DatagramReceivedEventArgs<byte[]> args)
        {
            Gate gate = sender as Gate;
            IPAddress ip = args.RemoteEndPoint.Address;
            int port = args.RemoteEndPoint.Port;
            List<Route> rts = gate.Routes;
            var vrs = rts.Where(rs => rs.From.IP.Equals(ip) && rs.From.Port == port);
            foreach (var vr in vrs)
            {

            }
        }
    }
}
