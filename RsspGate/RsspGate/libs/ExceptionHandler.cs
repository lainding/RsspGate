using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RsspGate.libs
{
    class ExceptionHandler
    {
        public static void Handle(Exception e)
        {
            if (e.GetBaseException().GetType() == typeof(ArgumentException))
            {
                Console.WriteLine("You caught an ArgumentException.");
            }
            else
            {
                //Console.WriteLine("You did not catch an exception.");
                //throw e;   // re-throwing is the default behavior
            }
        }
    }
}
