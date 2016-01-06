using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RsspGate.config;

namespace RsspGate.libs.operation
{
    class OperationFactory
    {
        public static Operation GetOperation(string name)
        {
            switch(name)
            {
                case "direct":
                    return new direct();
            }
            return null;
        }

        public static parameter GetParameter(string name, dynamic param)
        {
            switch(name)
            {
                case "direct":
                    return (direct_parameter)param;
            }
            return null;
        }
    }
}
