using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace OpenEsdh._2013.Outlook
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            
            OpenEsdh.Outlook.Model.Logging.Logger.Current.LogInformation("Application Startup");
            OpenEsdh.Outlook.Model.Container.OutlookResolver.Current = new OpenEsdh.Outlook.Model.Container.OutlookResolver(typeof(ThisAddIn));
            OpenEsdh.Outlook.Model.Container.OutlookResolver.Current.AddComponent<OpenEsdh._2013.Outlook.Presentation.Interface.ISaveEmailPresenter>(() =>
            {
                return new OpenEsdh._2013.Outlook.Presentation.Implementation.SaveEmailPresenter();
            });

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // This is not called - application just exits when outlook is shut down........
            OpenEsdh.Outlook.Model.Logging.Logger.Current.LogInformation("Application Shutdown");
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
