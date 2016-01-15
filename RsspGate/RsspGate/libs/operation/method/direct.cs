using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsspGate.config;

namespace RsspGate.libs.operation
{
    class direct : Operation
    {
        public override void Init(parameter param)
        {
            config.direct dp = param as config.direct;
        }


        public override byte[] Operate(byte[] stream)
        {
            var org = (byte[])stream.Clone();
            var result = org;
            if (this._nextOperation!=null)
            {
                result = _nextOperation.Operate(result);
            }
            return result;
        }
    }
}
