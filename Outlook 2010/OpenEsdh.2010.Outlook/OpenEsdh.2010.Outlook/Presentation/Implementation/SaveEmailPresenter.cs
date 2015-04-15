using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenEsdh._2010.Outlook.Model;
namespace OpenEsdh._2010.Outlook.Presentation.Implementation
{
    public class SaveEmailPresenter : OpenEsdh._2010.Outlook.Presentation.Interface.ISaveEmailPresenter
    {
        public void SaveEmailClick(dynamic Context)
        {
            if (Context.CurrentItem != null)
            {
                var presenter = OpenEsdh.Outlook.Model.Container.OutlookResolver.Current.Create<OpenEsdh.Outlook.Presenters.Interface.ISaveAsPresenter>();

                Microsoft.Office.Interop.Outlook.MailItem item = Context.CurrentItem as Microsoft.Office.Interop.Outlook.MailItem;
                if (item != null)
                {
                    presenter.Show(item.ToMailDescriptor());
                }
            }
        }
        public bool SaveEmailAndSend(dynamic Context)
        {
            if (Context.CurrentItem != null)
            {
                var presenter = OpenEsdh.Outlook.Model.Container.OutlookResolver.Current.Create<OpenEsdh.Outlook.Presenters.Interface.ISaveAsPresenter>();

                Microsoft.Office.Interop.Outlook.MailItem item = Context.CurrentItem as Microsoft.Office.Interop.Outlook.MailItem;
                if (item != null)
                {
                    bool result = presenter.ShowAndSend(item.ToMailDescriptor());
                    if(result)
                    {
                        item.Send();
                        item.Close(Microsoft.Office.Interop.Outlook.OlInspectorClose.olSave);
                    }
                    return result;
                }
            }
            return false;

        }
        public void Load(dynamic Context)
        {
            View.Visible = true;
            if (Context.CurrentItem != null)
            {
                Microsoft.Office.Interop.Outlook.MailItem item = Context.CurrentItem as Microsoft.Office.Interop.Outlook.MailItem;
                if (item == null)
                {
                    View.Visible = false;
                }
            }
        }
        public OpenEsdh._2010.Outlook.Presentation.Interface.ISaveEmailButtonView View { get; set; }
    }
}
