using OpenEsdh.Outlook.Model.ServerCertificate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OpenESDH.WebFormTest.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                OpenEsdh.Outlook.Model.Container.OutlookResolver.Current = new OpenEsdh.Outlook.Model.Container.OutlookResolver(this.GetType());

                TokenFetcher fetch = new TokenFetcher();
                textBox1.Text = fetch.GetParameters();
            }catch(Exception ex)
            {
                textBox1.Text = ex.Message;
            }
        }
    }
}
