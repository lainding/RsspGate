﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs.operation
{
    class direct : Operation
    {
        public override byte[] Operate(byte[] stream)
        {
            return stream;
        }
    }
}