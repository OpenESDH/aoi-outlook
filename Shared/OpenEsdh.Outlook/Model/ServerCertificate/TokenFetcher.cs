using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace OpenEsdh.Outlook.Model.ServerCertificate
{
    public class TokenFetcher:Model.IPreAuthenticator
    {
        private System.Collections.Specialized.NameValueCollection _additionalParameters = null;
        public System.Collections.Specialized.NameValueCollection AdditionalParameters
        {
            get 
            { 
                if(_additionalParameters==null)
                {
                    GenerateRequest();
                }
                return _additionalParameters;
            }
        }
        private void DoWebRequest(WebRequest webRequest)
        {

            webRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
            webRequest.PreAuthenticate = false;
            WebResponse webResponse = webRequest.GetResponse();
            using (System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
            {
                string s = reader.ReadToEnd();
                foreach(string key in webResponse.Headers.AllKeys)
                {
                    switch(key.ToLower())
                    {
                        case "authorization":
                            {
                                _additionalParameters.Add("Authorization", webResponse.Headers[s]);
                                break;
                            }
                        case "set-cookie":
                            {
                                _additionalParameters.Add("Cookie", webResponse.Headers[s]);
                                break;
                            }
                    }
                }
            }
        }
        private void GenerateRequest()
        {
            _additionalParameters = new System.Collections.Specialized.NameValueCollection();
            var _configuration = Model.Container.OutlookResolver.Current.Create<Model.Configuration.Interface.IOutlookConfiguration>();
            WebRequest webRequest = WebRequest.Create(_configuration.SaveAsDialogUrl);
            if(_configuration.PreAuthentication.UseConfigCredentials)
            {
                using(ImpersonationContext ctx=new ImpersonationContext(_configuration.PreAuthentication.Username,_configuration.PreAuthentication.Password,_configuration.PreAuthentication.Domain))
                {
                    WebClient client = new WebClient();
                    client.UseDefaultCredentials = true;
                    string s= client.DownloadString(_configuration.SaveAsDialogUrl);


                    DoWebRequest(webRequest);
                }
            }
            else
            {
                DoWebRequest(webRequest);
            }
        }

        public string GetParameters()
        {
            if (_additionalParameters == null)
            {
                GenerateRequest();
            }
            string delim = "";
            string returns = "";
            for (int i = 0; i < _additionalParameters.Count; i++)
            {
                returns += delim + _additionalParameters.GetKey(i) + "=" + _additionalParameters[_additionalParameters.GetKey(i)];
                delim = "&";
            }
            return returns;
        }
    }
}
