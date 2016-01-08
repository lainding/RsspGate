using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RsspGate.config
{
    class ConfigErrorException : Exception
    {
        private string message;
        public ConfigErrorException(string msg)
        {
            this.message = msg;
        }

        public override string Message
        {
            get
            {
                return message;
            }
        }
    }
}
