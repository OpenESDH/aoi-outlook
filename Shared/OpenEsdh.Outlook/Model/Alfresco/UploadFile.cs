using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;

namespace OpenEsdh.Outlook.Model.Alfresco
{
    public class UploadFile
    {
        public UploadFile()
        {
            ContentType = "application/octet-stream";
        }
        public string Name { get; set; }
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public Stream Stream { get; set; }
    }
    public class AlfrescoFilePost : OpenEsdh.Outlook.Model.Alfresco.IAlfrescoFilePost
    {
        private string _url="";
        public AlfrescoFilePost()
        {

        }
        public AlfrescoFilePost(string Url)
        {
            _url=Url;
        }
        private byte[] UploadFiles(string address, IEnumerable<UploadFile> files, NameValueCollection values)
        {
            Model.ServerCertificate.ICookieJar cookies = Model.Container.OutlookResolver.Current.Create<Model.ServerCertificate.ICookieJar>();
            HttpWebRequest request = HttpWebRequest.Create(address) as HttpWebRequest;
            request.AllowAutoRedirect = true;
            request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            request.PreAuthenticate = true;
            request.UseDefaultCredentials = true;

            try
            {
                request.CookieContainer = new CookieContainer();
                if (cookies.Cookies.Count > 0)
                {
                    foreach (string cookie in cookies.Cookies.Keys)
                    {
                        request.CookieContainer.Add(new Cookie(cookie, cookies.Cookies[cookie], "/", request.RequestUri.DnsSafeHost ));
                    }
                }
            }
            catch (Exception ex)
            {
                Model.Logging.Logger.Current.LogException(ex);
            }

            request.Method = "POST";
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            boundary = "--" + boundary;

            using (var requestStream = request.GetRequestStream())
            {
                // Write the values
                foreach (string name in values.Keys)
                {
                    var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(values[name] + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                }
                // Write the files
                foreach (var file in files)
                {
                    var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"filedata\"; filename=\"{0}\"{1}", file.Name, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    file.Stream.CopyTo(requestStream);
                    buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                var boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
                requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
            }

            try{
                using (var response = request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var stream = new MemoryStream())
                        {
                            responseStream.CopyTo(stream);
                            return stream.ToArray();
                        }
                    }
                }
            }catch
            {
                return null;
            }
        }
        public string UploadFile(string address,string unknownJson,string fileName,string name)
        {
            using (var stream = File.Open(fileName, FileMode.Open))
            {
                var values = new NameValueCollection
                {
                    {"metadata",unknownJson}
                };
                var files = new[]
                {
                    new UploadFile
                    {
                        Name=name,
                        Filename=Path.GetFileName(fileName),
                        ContentType=Attachment.GetMimeType(fileName) ,
                        Stream=stream
                    }
                };
                byte[] result = UploadFiles(address, files, values);
                return Encoding.ASCII.GetString(result);
            }
        }
        public string UploadFile(string unknownJson,string fileName,string Name)
        {
            return UploadFile(_url, unknownJson, fileName,Name);
        }
    }
}
