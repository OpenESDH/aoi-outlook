using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace OpenEsdh._2010.Outlook.Installer
{
    [RunInstaller(true)]
    public partial class OutlookInstaller : System.Configuration.Install.Installer
    {
        public OutlookInstaller()
        {
            InitializeComponent();
        }
        
    }
}
