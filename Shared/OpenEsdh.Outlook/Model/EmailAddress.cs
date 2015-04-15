using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEsdh.Outlook.Model
{
    public class EmailAddress
    {
        public EmailAddress()
        {
            EMail = "";
            Name = "";
        }
        public EmailAddress(string email, string name)
            : this()
        {
            EMail = email;
            Name = name;
        }
        public EmailAddress(string email)
            : this()
        {
            EMail = email;
            Name = email;
        }
        public string EMail { get; set; }
        public string Name { get; set; }
    }

}
