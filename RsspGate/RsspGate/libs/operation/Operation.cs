using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsspGate.config;
using RsspGate.libs.operation.addon;
using RsspGate.libs.operation.avaliable;

namespace RsspGate.libs.operation
{
    abstract class Operation
    {
        protected Addon _getValueWidget = new DefaultAddon();
        protected Avaliable _avaliableWidget = new DefaultAvaliable();
        protected Operation _nextOperation;
        public void SetNextOperation(Operation oper)
        {
            this._nextOperation = oper;
        }

        public abstract byte[] Operate(byte[] stream);
        public abstract void Init(parameter param);

        public void SetValueWidget(Addon widget)
        {
            this._getValueWidget = widget;
        }

        public void SetAvaliableWidget(Avaliable widget)
        {
            this._avaliableWidget = widget;
        }
    }
}
