using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;
using OpenEsdh._2013.Outlook.Model;
namespace OpenEsdh._2013.Outlook
{
    partial class OpenESDHRegion:OpenEsdh.Outlook.Views.Interface.IDisplayRegion
    {
        #region Form Region Factory

        //[Microsoft.Office.Tools.Outlook.FormRegionMessageClass(Microsoft.Office.Tools.Outlook.FormRegionMessageClassAttribute.Note)]
        [Microsoft.Office.Tools.Outlook.FormRegionMessageClass("IPM.Note.OpenESDH")]
        [Microsoft.Office.Tools.Outlook.FormRegionName("OpenEsdh.2013.Outlook.OpenESDHRegion")]
        public partial class OpenESDHRegionFactory
        {
            // Occurs before the form region is initialized.
            // To prevent the form region from appearing, set e.Cancel to true.
            // Use e.OutlookItem to get a reference to the current Outlook item.
            private void OpenESDHRegionFactory_FormRegionInitializing(object sender, Microsoft.Office.Tools.Outlook.FormRegionInitializingEventArgs e)
            {
                try
                {
                    this.Manifest.FormRegionName = OpenEsdh.Outlook.Model.Resources.ResourceResolver.Current.GetString("ViewRegionTitle");
                }catch(Exception ex)
                {
                    OpenEsdh.Outlook.Model.Logging.Logger.Current.LogException(ex);
                }
            }
        }

        #endregion
        
        // Occurs before the form region is displayed.
        // Use this.OutlookItem to get a reference to the current Outlook item.
        // Use this.OutlookFormRegion to get a reference to the form region.
        private OpenEsdh.Outlook.Presenters.Interface.IDisplayRegionPresenter _presenter = null;
        private void OpenESDHRegion_FormRegionShowing(object sender, System.EventArgs e)
        {
            
            _presenter = OpenEsdh.Outlook.Model.Container.OutlookResolver.Current.Create<OpenEsdh.Outlook.Presenters.Interface.IDisplayRegionPresenter>(this);
            var item=this.OutlookItem as Microsoft.Office.Interop.Outlook.MailItem;
            if (item != null)
            {
                _presenter.Show(item.ToMailDescriptor());
            }
            
        }

        // Occurs when the form region is closed.
        // Use this.OutlookItem to get a reference to the current Outlook item.
        // Use this.OutlookFormRegion to get a reference to the form region.
        private void OpenESDHRegion_FormRegionClosed(object sender, System.EventArgs e)
        {
        }

        private void OpenESDHRegion_Load(object sender, EventArgs e)
        {
            
            
        }

        public System.Collections.IList FormControlCollection 
        {
            get { return this.Controls; }
        }
    }
}
