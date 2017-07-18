using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SettingsConfigurator
{
    [RunInstaller(true)]
    public partial class Install : System.Configuration.Install.Installer
    {
        public Install()
        {
            InitializeComponent();
        }
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            if (Directory.Exists(EMSCacherConfiguartor.programData))
            {
                Directory.Delete(EMSCacherConfiguartor.programData, true);
            }
        }
    }
}
