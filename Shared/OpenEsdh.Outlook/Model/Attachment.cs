using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEsdh.Outlook.Model
{
    public class Attachment
    {
        public static string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        public Attachment()
        {
            Name = "";
            MimeType = "";
            ForceImport = false;
        }
        public Attachment(string name, string mimeType, bool forceImport)
            : this()
        {
            Name = name;
            MimeType = mimeType;
            ForceImport = forceImport;
        }
        public Attachment(string name, string mimeType)
            : this()
        {
            Name = name;
            MimeType = mimeType;
            ForceImport = false;
        }

        public string Name { get; set; }
        public string MimeType { get; set; }
        public bool ForceImport { get; set; }

    }
    public class SelectableAttachment:Attachment
    {
        public SelectableAttachment():base()
        {
            Selected = false;
        }
        public SelectableAttachment(string name, string mimeType, bool forceImport)
            : this()
        {
        }
        public SelectableAttachment(string name, string mimeType)
            : this()
        {
        }
        public bool Selected { get; set; }

    }

}
