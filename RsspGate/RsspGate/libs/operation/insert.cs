using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsspGate.config;

namespace RsspGate.libs.operation
{
    class insert : Operation
    {
        private int position;
        private int length;
        private byte[] bytes;
        public override void Init(parameter param)
        {
            config.insert p = param as config.insert;
            this.position = p.position;
            this.length = p.length;
            this.bytes = p.bytes;
        }

        public override byte[] Operate(byte[] stream)
        {
            var org = (byte[])stream.Clone();
            byte[] result = null;
            var p = position;
            var l = length;
            var bs = bytes;
            if (position == -1)
            {
                p = stream.Length;
            }
            if (org.Length >= p)
            {
                using (MemoryStream ms = new MemoryStream(stream.Length + l))
                {
                    ms.Write(org, 0, p);
                    ms.Write(bs, 0, l);
                    ms.Write(org, p, stream.Length - p);
                    result = ms.GetBuffer();
                }
            }
            else
            {
                result = org;
            }
            if (this._nextOperation != null)
            {
                result = _nextOperation.Operate(result);
            }
            return result;
        }
    }
}
