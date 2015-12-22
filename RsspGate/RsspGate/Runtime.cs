using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsspGate.config;

namespace RsspGate
{
    static class Runtime
    {
        static List<inter> inters = new List<inter>();
        public static List<inter> Interface
        {
            get
            {
                return inters;
            }
        }
    }
}
