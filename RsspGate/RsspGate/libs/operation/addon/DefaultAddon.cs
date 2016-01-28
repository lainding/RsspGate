using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs.operation.addon
{
    class DefaultAddon : Addon
    {

        public override int GetLength(byte[] stream)
        {
            return 0;
        }

        public override byte[] GetValue(byte[] stream)
        {
            return new byte[] { };
        }
    }
}
