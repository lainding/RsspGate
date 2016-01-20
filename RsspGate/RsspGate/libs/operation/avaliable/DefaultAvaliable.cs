using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs.operation.avaliable
{
    class DefaultAvaliable : Avaliable
    {
        public override bool IsAvaliable(byte[] stream)
        {
            return true;
        }
    }
}
