using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsspGate.config;

namespace RsspGate.libs.operation
{
    class remove : Operation
    {
        private int position;
        private int length;
        public override void Init(parameter param)
        {
            config.remove p = param as config.remove;
            this.position = p.position;
            this.length = p.length;
        }

        public override byte[] Operate(byte[] stream)
        {
            var org = new List<byte>(stream);
            byte[] result = null;
            var p = position;
            var l = length;
            if (position == -1)
            {
                p = stream.Length - l;
            }
            if (org.Count >= (p + l))
            {
                org.RemoveRange(p, l);
            }
            else
            {

            }
            result = org.ToArray();
            if (this._nextOperation != null)
            {
                result = _nextOperation.Operate(result);
            }
            return result;
        }
    }
}
