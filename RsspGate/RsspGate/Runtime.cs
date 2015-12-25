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
        public static List<AsyncUdpServer> interfaces = new List<AsyncUdpServer>();

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
            var index = 0;
            foreach (var i in cfg.interfaces)
            {
                i.index = index++;
                if (i.name == null)
                {
                    i.name = "interface." + i.index;
                }
                try
                {
                    IPAddress ip = IPAddress.Parse(i.ip);
                    AsyncUdpServer inter = new AsyncUdpServer(ip, i.port);
                    inter.DatagramReceived += InterfaceProcess.ProcessUDPInput;
                    interfaces.Add(inter);
                }
                catch(Exception e)
                {
                    ExceptionHandler.Handle(e);
                }
            }
        }
    }
}
