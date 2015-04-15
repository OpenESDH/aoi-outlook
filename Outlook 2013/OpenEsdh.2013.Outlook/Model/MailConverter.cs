using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEsdh._2013.Outlook.Model
{
    public static class MailConverter
    {

        public static OpenEsdh.Outlook.Model.EmailDescriptor ToMailDescriptor(this Microsoft.Office.Interop.Outlook.MailItem mailItem)
        {
            OpenEsdh.Outlook.Model.EmailDescriptor returns = new OpenEsdh.Outlook.Model.EmailDescriptor();
            returns.Subject = mailItem.Subject;
            returns.BodyText = mailItem.Body;
            returns.BodyHtml = mailItem.HTMLBody;
            if (mailItem.Sender != null)
            {
                returns.From = new OpenEsdh.Outlook.Model.EmailAddress(mailItem.Sender.Address);
            }
            else
            {
                returns.From = new OpenEsdh.Outlook.Model.EmailAddress();
            }
            if (mailItem.To != null)
            {
                foreach (var to in mailItem.To.Split(';'))
                {
                    returns.To.Add(new OpenEsdh.Outlook.Model.EmailAddress(to));
                }
            }
            if (mailItem.CC != null)
            {
                foreach (var to in mailItem.CC.Split(';'))
                {
                    returns.CC.Add(new OpenEsdh.Outlook.Model.EmailAddress(to));
                }
            }
            if (mailItem.BCC != null)
            {
                foreach (var to in mailItem.BCC.Split(';'))
                {
                    returns.BCC.Add(new OpenEsdh.Outlook.Model.EmailAddress(to));
                }
            }
            if (mailItem.Attachments != null)
            {
                foreach (Microsoft.Office.Interop.Outlook.Attachment attachment in mailItem.Attachments)
                {
                    returns.Attachments.Add(new OpenEsdh.Outlook.Model.Attachment(attachment.DisplayName, OpenEsdh.Outlook.Model.Attachment.GetMimeType(attachment.FileName)));
                }
                
            }
            if(mailItem.ItemProperties!=null)
            {
                foreach(Microsoft.Office.Interop.Outlook.ItemProperty property in mailItem.ItemProperties)
                {
                    try
                    {
                        if (property.Value != null)
                        {
                            string value = property.Value.ToString();
                            returns.MetaData.Add(new KeyValuePair<string, string>(property.Name, value));
                        }
                    }catch(Exception ex)
                    {
                        OpenEsdh.Outlook.Model.Logging.Logger.Current.LogException(ex);
                    }
                }
            }

            return returns;

        }
    }
}
