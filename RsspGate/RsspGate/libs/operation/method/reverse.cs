using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsspGate.config;

namespace RsspGate.libs.operation
{
    class reverse : Operation
    {
        private int begin;
        private int end;
        public override void Init(parameter param)
        {
            config.reverse c = param as config.reverse;
            begin = c.begin;
            end = c.end;
        }

        public override byte[] Operate(byte[] stream)
        {
            var org = (byte[])stream.Clone();
            var t_end = end;
            var t_begin = begin;
            if (end == -1)
            {
                t_end = stream.Length - 1;
            }
            if (begin == -1)
            {
                t_begin = stream.Length - 1;
            }
            if (t_end < t_begin)
            {
                var tmp = t_end;
                t_end = t_begin;
                t_begin = tmp;
            }
            if (t_end < stream.Length)
            {
                Array.Reverse(org, t_begin, t_end - t_begin + 1);
            }
            else
            {

            }
            return org;
        }
    }
}
