using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;

namespace OpenEsdh.Outlook.Model
{
    public interface IPreAuthenticator
    {
        System.Collections.Specialized.NameValueCollection  AdditionalParameters { get; }
        string GetParameters();

    }

    public class PreAuthenticator : OpenEsdh.Outlook.Model.IPreAuthenticator
    {
        
        private Configuration.Interface.IOutlookConfiguration _configuration;
        private System.Collections.Specialized.NameValueCollection _additionalParameters = null;
        public PreAuthenticator(Configuration.Interface.IOutlookConfiguration configuration)
        {
            _configuration = configuration;
        }
        public System.Collections.Specialized.NameValueCollection AdditionalParameters
        {
            get
            {
                if(_additionalParameters==null)
                {
                    PreAuthenticate();
                }
                return _additionalParameters;

            }
        }
        public string GetParameters()
        {
            if(_additionalParameters==null)
            {
                PreAuthenticate();
            }
            string delim = "";
            string returns = "";
            for(int i=0;i<_additionalParameters.Count;i++)
            {
                returns += delim + _additionalParameters.GetKey(i) + "=" + _additionalParameters[_additionalParameters.GetKey(i)];
                delim = "&";
            }
            return returns;
        }
        private void PreAuthenticate()
        {
            try
            {
                _additionalParameters = new System.Collections.Specialized.NameValueCollection();
                System.Net.WebClient client = new System.Net.WebClient();
                string userName = "";
                string passWord = "";
                if (_configuration.PreAuthentication.UseConfigCredentials)
                {
                    userName = _configuration.PreAuthentication.Username;
                    passWord = _configuration.PreAuthentication.Password;
                }
                else
                {
                    userName = System.Net.CredentialCache.DefaultNetworkCredentials.UserName;
                    // You wont actually get a password
                    passWord = System.Net.CredentialCache.DefaultNetworkCredentials.Password;
                }
                string request = _configuration.PreAuthentication.AuthenticationPackageFormat.Replace("[@username]", userName).Replace("[@password]", passWord);
                string response = client.UploadString(_configuration.PreAuthentication.AuthenticationUrl, request);
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                Data respObj = serializer.Deserialize<Data>(response);
                _additionalParameters.Add(_configuration.PreAuthentication.PreAuthenticateParameterName, respObj.data.ticket);
            }catch(Exception ex)
            {
                Logging.Logger.Current.LogException(ex);
            }

        }
        public class DataTicket
        {
            public string ticket { get; set; }
        }
        public class Data
        {
            public DataTicket data { get; set; }
        }
    }
}
