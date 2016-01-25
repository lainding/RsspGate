using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RsspGate.libs;

namespace RsspGate
{
    class InterfaceProcess
    {
        public static void ProcessInput(object sender, DatagramReceivedEventArgs<byte[]> args)
        {
            crc16 c = new crc16();
            var a = c.ComputeChecksumBytes(args.Datagram);
            Gate gate = sender as Gate;
            IPAddress ip = args.RemoteEndPoint.Address;
            int port = args.RemoteEndPoint.Port;
            List<Route> rts = gate.Routes;
            var vrs = rts.Where(rs => rs.From.IP.Equals(ip) && rs.From.Port == port);
            foreach (var vr in vrs)
            {
                if (vr.Process != null)
                {
                    var result = vr.Process.Operate(args.Datagram);
                    vr.By.Send(vr.To, result);
                }
            }
        }
    }
}
