using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEsdh.Outlook.Model
{
    public class EmailDescriptor
    {
        public EmailAddress From { get; set; }
        public IList<EmailAddress> To { get; set; }
        public IList<EmailAddress> CC { get; set; }
        public IList<EmailAddress> BCC { get; set; }
        public IList<KeyValuePair<string, string>> MetaData { get; set; }

        public string Subject { get; set; }
        public string BodyText { get; set; }
        public string BodyHtml { get; set; }

        public IList<Attachment> Attachments { get; set; }

        //TODO: Parse XML Filevalues 

        public EmailDescriptor()
        {
            From = new EmailAddress();
            To = new List<EmailAddress>();
            CC = new List<EmailAddress>();
            BCC = new List<EmailAddress>();
            MetaData = new List<KeyValuePair<string, string>>();
            Attachments = new List<Attachment>();
            Subject = "";
            BodyText = "";
            BodyHtml = "";
        }

    }


}
