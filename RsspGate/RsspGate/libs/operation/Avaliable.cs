using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.libs.operation
{
    public abstract class Avaliable
    {
        public bool IsAvaliable()
        {
            return true;
        }
        private Avaliable _andNext;
        private Avaliable _orNext;
        private bool isNot;
    }
}
