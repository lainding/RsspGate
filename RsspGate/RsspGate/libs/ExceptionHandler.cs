using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RsspGate.libs
{
    class ExceptionHandler
    {
        public static void Handle(Exception e)
        {
//            if (e.GetBaseException().GetType() == typeof(ArgumentException))
//            {
//                Console.WriteLine("You caught an ArgumentException.");
//            }
//            else
            {
                //Console.WriteLine("You did not catch an exception.");
                //throw e;   // re-throwing is the default behavior
                MessageBox.Show(e.Message);
                Runtime.IsRunning = false;
                Application.Exit();
            }
        }
    }
}
