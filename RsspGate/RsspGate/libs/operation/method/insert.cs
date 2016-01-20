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
        public override void Init(parameter param)
        {
            config.insert p = param as config.insert;
            this.position = p.position;
            //this.bytes = p.bytes;
        }

        public override byte[] Operate(byte[] stream)
        {
            var org = (byte[])stream.Clone();
            byte[] result = new byte[0] { };
            if (this._avaliableWidget.IsAvaliable(stream))
            {
                var p = position;
                var l = this._getValueWidget.GetLength();
                var bs = this._getValueWidget.GetValue();
                if (position == -1)
                {
                    p = stream.Length;
                }
                if (org.Length >= p)
                {
                    if (bs != null)
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
                        //TODO: log
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
            }
            return result;
        }
    }
}
