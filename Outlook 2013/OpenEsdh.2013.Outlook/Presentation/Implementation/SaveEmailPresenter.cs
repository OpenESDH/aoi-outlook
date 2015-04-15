using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenEsdh._2013.Outlook.Model;
namespace OpenEsdh._2013.Outlook.Presentation.Implementation
{
    public class SaveEmailPresenter : OpenEsdh._2013.Outlook.Presentation.Interface.ISaveEmailPresenter
    {
        public void SaveEmailClick(dynamic Context)
        {
            if (Context.CurrentItem != null)
            {
                var presenter = OpenEsdh.Outlook.Model.Container.OutlookResolver.Current.Create<OpenEsdh.Outlook.Presenters.Interface.ISaveAsPresenter>();

                Microsoft.Office.Interop.Outlook.MailItem item = Context.CurrentItem as Microsoft.Office.Interop.Outlook.MailItem;
                if (item != null)
                {
                    presenter.SetMessageClass += (messageClass) =>
                    {
                        item.MessageClass = messageClass;
                        item.Save();
                    };
                    presenter.SetMessageID += (messageID) =>
                        {
                            if (item.ItemProperties[OpenEsdh.Outlook.Model.Configuration.Constants.IDColumn] == null)
                            {
                                item.ItemProperties.Add(OpenEsdh.Outlook.Model.Configuration.Constants.IDColumn, Microsoft.Office.Interop.Outlook.OlUserPropertyType.olText, Type.Missing, Type.Missing);
                            }
                            item.ItemProperties[OpenEsdh.Outlook.Model.Configuration.Constants.IDColumn].Value=messageID;
                            item.Save();
                        };
                    /*presenter.SaveMailBody += (Upload) =>
                        {
                            string filename ="";
                            string path = System.IO.Path.GetTempFileName();
                            if(!string.IsNullOrWhiteSpace(item.HTMLBody))
                            {
                                filename = path + ".html";
                                item.SaveAs(filename,Microsoft.Office.Interop.Outlook.OlSaveAsType.olHTML);
                            }else
                            {
                                filename = path + ".txt";
                                item.SaveAs(filename, Microsoft.Office.Interop.Outlook.OlSaveAsType.olTXT);
                            }
                            Upload(filename);
                            try
                            {
                                System.IO.File.Delete(filename,att.FileName);
                            }catch(Exception ex)
                            {
                                OpenEsdh.Outlook.Model.Logging.Logger.Current.LogException(ex);
                            }
                        };*/
                    presenter.SaveAttachment+=(name,Upload)=>
                    {
                        string filename = "";
                        string path = System.IO.Path.GetTempFileName();

                        for (int i = 1; i <= item.Attachments.Count;i++ )
                        {
                            Microsoft.Office.Interop.Outlook.Attachment att = item.Attachments[i];
                            if(att.DisplayName==name)
                            {
                                string ext = System.IO.Path.GetExtension(att.FileName);
                                if(!ext.StartsWith("."))
                                {
                                    ext = "." + ext;
                                }
                                filename = path + ext;
                                att.SaveAsFile(filename);
                                Upload(filename,att.FileName);
                                break;
                            }
                        }
                        if (!string.IsNullOrEmpty(filename))
                        {
                            try
                            {
                                System.IO.File.Delete(filename);
                            }
                            catch (Exception ex)
                            {
                                OpenEsdh.Outlook.Model.Logging.Logger.Current.LogException(ex);
                            }
                        }

                    };
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
                    presenter.SetMessageClass += (messageClass) =>
                    {
                        item.MessageClass = messageClass;
                        item.Save();
                    };
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
        public OpenEsdh._2013.Outlook.Presentation.Interface.ISaveEmailButtonView View { get; set; }
    }
}
