using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs
{
    abstract class DataFrame
    {
        public abstract void Serialize();
        public abstract void DeSerialize();
    }
}
