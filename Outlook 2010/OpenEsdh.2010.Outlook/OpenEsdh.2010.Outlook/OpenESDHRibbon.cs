using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace OpenEsdh._2010.Outlook
{
    public partial class OpenESDHRibbon : OpenEsdh._2010.Outlook.Presentation.Interface.ISaveEmailButtonView
    {
        private OpenEsdh._2010.Outlook.Presentation.Interface.ISaveEmailPresenter _presenter;

        private void OpenESDHRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            try
            {
                _presenter = OpenEsdh.Outlook.Model.Container.OutlookResolver.Current.Create<OpenEsdh._2010.Outlook.Presentation.Interface.ISaveEmailPresenter>();
                _presenter.View = this;
                _presenter.Load(this.Context);
            }catch(Exception ex)
            {
                OpenEsdh.Outlook.Model.Logging.Logger.Current.LogException(ex);
                throw ex;
            }

        }

        private void SaveEmailBtn_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                _presenter.SaveEmailClick(this.Context);
            }catch(Exception ex)
            {
                OpenEsdh.Outlook.Model.Logging.Logger.Current.LogException(ex);
                throw ex;
            }
        }
        private void SaveAndSendEmailBtn_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                bool result = _presenter.SaveEmailAndSend(this.Context);
                if (result)
                {

                }
            }catch(Exception ex)
            {
                OpenEsdh.Outlook.Model.Logging.Logger.Current.LogException(ex);
                throw ex;
            }
        }

        public bool Visible
        {
            get
            {
                return this.group1.Visible;
            }
            set
            {
                this.group1.Visible = value;
                this.group2.Visible = value;
            }
        }

    }
}
