using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs
{
    class Device
    {
        public IPAddress IP
        {
            get; set;
        }

        public int Port
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public Device(string name, IPAddress ip, int port)
        {
            this.Name = name;
            this.IP = ip;
            this.Port = port;
        }

        public override bool Equals(object obj)
        {
            Device d = obj as Device;
            return d.IP == this.IP && d.Port == this.Port;
        }
    }
}
