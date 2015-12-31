using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RsspGate.libs.operation;

namespace RsspGate.libs
{
    class Route
    {
        public Device From
        {
            get; set;
        }
        public Device To
        {
            get; set;
        }
        public Gate Through
        {
            get; set;
        }
        public Gate By
        {
            get; set;
        }
        public Operation Process
        {
            get; set;
        }

        public Route(Device from, Gate through, Device to, Gate by)
        {
            this.From = from;
            this.Through = through;
            this.To = to;
            this.By = by;
        }
    }
}
