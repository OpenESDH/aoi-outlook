using System;
using System.Windows.Forms;

namespace WebBrowserControlDialogs
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
            
            // Tell the WidowsInterop to Hook messages
            WindowsInterop.Hook();

            Application.Run(new MainForm());

            // Tell the WidowsInterop to Unhook
            WindowsInterop.Unhook();
        }
    }
}
