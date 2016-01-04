using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs.operation
{
    class direct : Operation
    {
        public override void Init()
        {

        }

        public override byte[] Operate(byte[] stream)
        {
            var org = (byte[])stream.Clone();
            org[0] = (byte)'H';
            var result = org;
            if (this._nextOperation!=null)
            {
                result = _nextOperation.Operate(result);
            }
            return result;
        }
    }
}
