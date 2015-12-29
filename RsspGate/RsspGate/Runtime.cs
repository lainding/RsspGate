using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RsspGate.config;
using RsspGate.libs;

namespace RsspGate
{
    static class Runtime
    {
        public static List<Gate> gates = new List<Gate>();
        public static List<Device> devices = new List<Device>();

        private static bool isRunning = true;
        public static bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
            }
        }
        public static void ProcessConfig(config.config cfg)
        {
            if (cfg == null)
            {
                return;
            }
            foreach (var g in cfg.gates)
            {
                try
                {
                    switch (g.type.ToLower())
                    {
                        case "udp":
                            {
                                IPAddress ip = IPAddress.Parse(g.ip);
                                AsyncUdpServer inter = new AsyncUdpServer(ip, g.port);
                                inter.DatagramReceived += InterfaceProcess.ProcessUDPInput;
                                gates.Add(inter);
                                break;
                            }
                        default:
                            throw new ArgumentException("Unknown gate type.");
                    }
                }
                catch(Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
            foreach (var d in cfg.devices)
            {
                try
                {
                    IPAddress ip = IPAddress.Parse(d.ip);
                    Device dv = new Device(d.name, ip, d.port);
                    devices.Add(dv);
                }
                catch(Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
            foreach(var r in cfg.routes)
            {

            }
        }
    }
}
