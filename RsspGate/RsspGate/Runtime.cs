using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RsspGate.config;
using RsspGate.libs;
using RsspGate.libs.operation;

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

        private static void ProcessGates(dynamic gates)
        {
                foreach (var gate in gates)
                {
                    gate g = (gate)gate;
                    if (g.name == null)
                    {
                        throw new ConfigErrorException("Config file gates item miss 'name' parameters.");
                    }
                    if (g.type != null)
                    {
                        switch (g.type.ToLower())
                        {
                            case "udp":
                                {
                                    IPAddress ip = IPAddress.Parse(g.ip);
                                    Gate inter = new AsyncUdpServer(ip, g.port);
                                    inter.Name = g.name;
                                    inter.DatagramReceived += InterfaceProcess.ProcessInput;
                                    gates.Add(inter);
                                    break;
                                }
                            default:
                                throw new ArgumentException("Unknown gate type.");
                        }
                    }
                    else
                    {
                        throw new ConfigErrorException("Config file gates item miss 'type' parameters.");
                    }
                }

        }

        private static void ProcessDevices(dynamic devices)
        {
            foreach (var device in devices)
            {
                device d = (device)device;
                if (d.name == null)
                {
                    throw new ConfigErrorException("Config file devices item miss 'name' parameters.");
                }
                IPAddress ip = IPAddress.Parse(d.ip);
                Device dv = new Device(d.name, ip, d.port);
                devices.Add(dv);
            }
        }

        private static void ProcessRoutes(dynamic routes)
        {

        }

        public static void ProcessConfig(dynamic cfg)
        {
            if (cfg == null)
            {
                return;
            }
            //IEnumerable<string> aas = cfg.GetDynamicMemberNames();
            //var matchRegex =new System.Text.RegularExpressions.Regex("gates", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //var havegates = aas.Where(ra => matchRegex.Matches(ra).Count > 0).ToList<string>();
            if (cfg.gates())
            {
                ProcessGates(cfg.gates);
            }
            else
            {
                throw new ConfigErrorException("Config file miss 'gates' parameters.");
            }

            if (cfg.devices())
            {
                ProcessDevices(cfg.devices);
            }
            else
            {
                throw new ConfigErrorException("Config file miss 'devices' parameters.");
            }

            if (cfg.routes())
            {
                ProcessRoutes(cfg.routes);
            }
            else
            {
                throw new ConfigErrorException("Config file miss 'routes' parameters.");
            }


            foreach(var r in cfg.routes)
            {
                try
                {
                    var from = devices.Where(ra => ra.Name == r.from);
                    var to = devices.Where(ra => ra.Name == r.to);
                    if (r.through == null && cfg.gates.Count != 1)
                    {
                        throw new ArgumentException("Miss Through or By Arguments.");
                    }
                    else
                    {
                        if (r.through == null)
                        {
                            r.through = cfg.gates[0].name;
                        }
                    }
                    if (r.by == null && cfg.gates.Count != 1)
                    {
                        throw new ArgumentException("Miss Through or By Arguments.");
                    }
                    else
                    {
                        if (r.by == null)
                        {
                            r.by = cfg.gates[0].name;
                        }
                    }
                    var through = gates.Where(ra => ra.Name == r.through);
                    var by = gates.Where(ra => ra.Name == r.by);
                    if (through.Count()!=1 || by.Count()!=1)
                    {
                        throw new ArgumentException("Multi same name in gates.");
                    }
                    else
                    {
                        foreach (var fi in from)
                        {
                            foreach(var ti in to)
                            {
                                Route route = new Route(fi, through.ToArray()[0], ti, by.ToArray()[0]);
                                Operation oper = null;
                                foreach(dynamic pi in r.process)
                                {
                                    if (pi.name())
                                    {
                                        var tmp = OperationFactory.GetOperation(pi.name);
                                        parameter param = null;
                                        if (pi.parameters())
                                        {
                                            param = OperationFactory.GetParameter(pi.name, pi.parameters);
                                        }
                                        if (tmp != null)
                                        {
                                            if (oper == null)
                                            {
                                                oper = tmp;
                                            }
                                            else
                                            {
                                                oper.SetNextOperation(tmp);
                                            }
                                            if (param != null)
                                            {
                                                tmp.Init(param);
                                            }
                                        }
                                    }
                                }
                                route.Process = oper;
                                through.ToArray()[0].AddRoute(route);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                }
            }
        }

    }
}
