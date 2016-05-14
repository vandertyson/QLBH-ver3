using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLBH.Forms;

namespace QLBH
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new f11_bao_cao_doanh_thu());
            //Application.Run(new Test());
            Application.Run(new f01_main_form());
        }
    }
}
