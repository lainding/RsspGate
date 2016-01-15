using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsspGate.config;

namespace RsspGate.libs.operation
{
    abstract class Operation
    {
        protected IGetValue _getValueWidget = null;
        protected Avaliable _avaliableWidget = null;
        protected Operation _nextOperation;
        public void SetNextOperation(Operation oper)
        {
            this._nextOperation = oper;
        }

        public abstract byte[] Operate(byte[] stream);
        public abstract void Init(parameter param);

        public void SetValueWidget(IGetValue widget)
        {
            this._getValueWidget = widget;
        }

        public void SetAvaliableWidget(Avaliable widget)
        {
            this._avaliableWidget = widget;
        }
    }
}
