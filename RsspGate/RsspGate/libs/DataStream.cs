using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs
{
    class DataStream
    {
        public IPAddress IPAddress { get; set; }
        int Port { get; set; }
        int Cycle { get; set; }
    }
}
