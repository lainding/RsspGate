﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsspGate.config;

namespace RsspGate.libs.operation
{
    class reverse : Operation
    {
        public override void Init(parameter param)
        {
            throw new NotImplementedException();
        }

        public override byte[] Operate(byte[] stream)
        {
            //stream.Reverse()
            return stream;
        }
    }
}