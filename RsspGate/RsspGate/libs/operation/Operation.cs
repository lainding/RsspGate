﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsspGate.config;

namespace RsspGate.libs.operation
{
    abstract class Operation
    {
        protected Operation _nextOperation;
        public void SetNextOperation(Operation oper)
        {
            this._nextOperation = oper;
        }

        public abstract byte[] Operate(byte[] stream);
        public abstract void Init(parameter param);
    }
}
