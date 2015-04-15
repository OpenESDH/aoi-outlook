using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenEsdh.Outlook.Views.Implementation
{
    public partial class DisplayRegionControl : UserControl, OpenEsdh.Outlook.Views.Interface.IDisplayRegionControl
    {
        public DisplayRegionControl()
        {
            InitializeComponent();
        }
        public void Show(string url)
        {
            this.webBrowser1.Navigate(url);
        }
    }
}
