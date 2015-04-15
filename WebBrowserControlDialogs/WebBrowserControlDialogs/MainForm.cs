using System;
using System.Windows.Forms;

namespace WebBrowserControlDialogs
{
    public partial class MainForm : Form
    {
        #region << Constructors >>

        public MainForm()
        {
            InitializeComponent();

            // Subscribe to Event(s) with the WindowsInterop Class
            WindowsInterop.SecurityAlertDialogWillBeShown +=
                new GenericDelegate<Boolean, Boolean>(this.WindowsInterop_SecurityAlertDialogWillBeShown);

            WindowsInterop.ConnectToDialogWillBeShown +=
                new GenericDelegate<String, String, Boolean>(this.WindowsInterop_ConnectToDialogWillBeShown);

            // Subscribe to the WebBrowser Control's DocumentCompleted event
            this.webBrowser1.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);

            navigateTo("http://www.codeproject.com");
        }

        #endregion

        #region << Local Event Handlers >>

        private void btnTestNoCredentialsDialog_Click(Object sender, EventArgs e)
        {
            // (Fill in the blanks with a Url for a page of choice where 
            // credentials would normally have to be entered manually)
            String sUrl = "";

            if (String.IsNullOrEmpty(sUrl))
            {
                MessageBox.Show(this, "Please provide a Url in the Source Code", 
                    "btnTestNoCredentialsDialog_Click(): Error!", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            navigateTo(sUrl);
        }

        private void btnTestNoSecurityAlertDialog_Click(Object sender, EventArgs e)
        {
            // (Fill in the blanks with a Url for a page of choice 
            // where an invalid SSL dialog would normally show)
            String sUrl = "";

            if (String.IsNullOrEmpty(sUrl))
            {
                MessageBox.Show(this, "Please provide a Url in the Source Code", 
                    "btnTestNoSecurityAlertDialog_Click(): No URL Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            navigateTo(sUrl);
        }

        private Boolean WindowsInterop_SecurityAlertDialogWillBeShown(Boolean blnIsSSLDialog)
        {
            // Return true to ignore and not show the 
            // "Security Alert" dialog to the user
            return true;
        }

        private Boolean WindowsInterop_ConnectToDialogWillBeShown(ref String sUsername, ref String sPassword)
        {
            // (Fill in the blanks in order to be able 
            // to return the appropriate Username and Password)
            sUsername = "";
            sPassword = "";

            // Return true to auto populate credentials and not 
            // show the "Connect To ..." dialog to the user
            return true;
        }

        private void webBrowser1_DocumentCompleted(Object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Enable the 2 buttons on the form
            this.btnTestNoCredentialsDialog.Enabled = true;
            this.btnTestNoSecurityAlertDialog.Enabled = true;

            this.Cursor = Cursors.Default;
        }

        #endregion

        #region << Private Parts >>

        private void navigateTo(String sUrl)
        {
            this.Cursor = Cursors.WaitCursor;

            this.btnTestNoCredentialsDialog.Enabled = false;
            this.btnTestNoSecurityAlertDialog.Enabled = false;

            this.webBrowser1.Navigate(sUrl);
        }

        #endregion
    }
}
