using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace OpenEsdh._2013.Outlook
{
    public partial class OpenESDHRibbon : OpenEsdh._2013.Outlook.Presentation.Interface.ISaveEmailButtonView
    {
        private OpenEsdh._2013.Outlook.Presentation.Interface.ISaveEmailPresenter _presenter;

        private void OpenESDHRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            try
            {
                _presenter = OpenEsdh.Outlook.Model.Container.OutlookResolver.Current.Create<OpenEsdh._2013.Outlook.Presentation.Interface.ISaveEmailPresenter>();
                _presenter.View = this;
                
                _presenter.Load(this.Context);
                group1.Label = OpenEsdh.Outlook.Model.Resources.ResourceResolver.Current.GetString("OpenESDHButtonGroup");
                group2.Label = OpenEsdh.Outlook.Model.Resources.ResourceResolver.Current.GetString("OpenESDHButtonGroup");
                btnSaveAsSend.Label = OpenEsdh.Outlook.Model.Resources.ResourceResolver.Current.GetString("SaveSendBtn");
                btnSaveFile.Label = OpenEsdh.Outlook.Model.Resources.ResourceResolver.Current.GetString("SaveBtn");
            }
            catch (Exception ex)
            {
                OpenEsdh.Outlook.Model.Logging.Logger.Current.LogException(ex);
                throw ex;
            }
        }

        public bool Visible
        {
            get
            {
                return group1.Visible;
            }
            set
            {
                group1.Visible = value;
                group2.Visible = value;
            }
        }

        private void btnSaveFile_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                bool result = _presenter.SaveEmailAndSend(this.Context);
                if (result)
                {

                }
            }
            catch (Exception ex)
            {
                OpenEsdh.Outlook.Model.Logging.Logger.Current.LogException(ex);
                throw ex;
            }

        }

        private void btnSaveAsSend_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                _presenter.SaveEmailClick(this.Context);
                try
                {
                    Microsoft.Office.Interop.Outlook.MailItem item = e.Control.Context.CurrentItem as Microsoft.Office.Interop.Outlook.MailItem;
                    var inspect = item.GetInspector as Microsoft.Office.Interop.Outlook.Inspector;
                    if(inspect!=null)
                    {
                        inspect.ShowFormPage(OpenEsdh.Outlook.Model.Resources.ResourceResolver.Current.GetString("ViewRegionTitle"));
                        inspect.Display();
                    }
                }catch
                {

                }
            }
            catch (Exception ex)
            {
                OpenEsdh.Outlook.Model.Logging.Logger.Current.LogException(ex);
                throw ex;
            }

        }
    }
}
