using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SettingsConfigurator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            if (arguments.Length == 1 && arguments[0] == "--detach")
            {
                Process.Start(Application.ExecutablePath, "--installFinished");
            }
            else
            {
                if (arguments.Length == 1 && arguments[0] == "--installFinished")
                {
                    MessageBox.Show("Installation Complete. You will now be able to tweak the settings of EMS Cacher. (Added to the Start Menu).");
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new EMSCacherConfiguartor());
            }
        }
    }
}
