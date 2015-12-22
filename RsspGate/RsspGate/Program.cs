using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RsspGate.config;
using RsspGate.libs;

namespace RsspGate
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AsyncUdpServer us = new AsyncUdpServer(9001);
            us.Start();
            Application.Run(new Form1());
        }
    }
}
