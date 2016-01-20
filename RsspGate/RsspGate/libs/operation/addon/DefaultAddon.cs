using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs.operation.addon
{
    class DefaultAddon : Addon
    {

        public override int GetLength()
        {
            return 0;
        }

        public override byte[] GetValue()
        {
            return new byte[] { };
        }
    }
}
