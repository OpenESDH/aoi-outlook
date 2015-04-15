using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace OpenEsdh.Outlook.Model.ServerCertificate
{
    public class CookieJar : OpenEsdh.Outlook.Model.ServerCertificate.ICookieJar
    {
        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetGetCookie(
          string url, string cookieName, StringBuilder cookieData, ref int size);
        private const int INTERNET_COOKIE_HTTPONLY = 0x00002000;
        
        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetGetCookieEx(
            string url,
            string cookieName,
            StringBuilder cookieData,
            ref int size,
            int flags,
            IntPtr pReserved);


        private Dictionary<string, string> _cookies = new Dictionary<string, string>();

        private CookieContainer GetUriCookieContainer(Uri uri)
        {
            CookieContainer cookies = null;

            // Determine the size of the cookie
            int datasize = 256;
            StringBuilder cookieData = new StringBuilder(datasize);

            if (!InternetGetCookie(uri.ToString(), null, cookieData,
              ref datasize))
            {
                if (datasize < 0)
                    return null;

                // Allocate stringbuilder large enough to hold the cookie
                cookieData = new StringBuilder(datasize);
                if (!InternetGetCookie(uri.ToString(), null, cookieData,
                  ref datasize))
                    return null;
            }

            if (cookieData.Length > 0)
            {
                cookies = new CookieContainer();
                cookies.SetCookies(uri, cookieData.ToString().Replace(';', ','));
            }
            return cookies;
        }
        public CookieContainer GetUriCookieContainerEx(Uri uri)
        {
            CookieContainer cookies = null;

            int size = 512;
            StringBuilder sb = new StringBuilder(size);
            if (!InternetGetCookieEx(uri.ToString(), null, sb, ref size, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero))
            {
                if (size < 0)
                {
                    return null;
                }
                sb = new StringBuilder(size);
                if (!InternetGetCookieEx(uri.ToString(), null, sb, ref size, INTERNET_COOKIE_HTTPONLY, IntPtr.Zero))
                {
                    return null;
                }
            }
            if (sb.Length > 0)
            {
                cookies = new CookieContainer();
                cookies.SetCookies(uri, sb.ToString().Replace(';', ','));
            }
            return cookies;
        }


        public void AddCookiesForUri(Uri uri)
        {
            var cookies = GetUriCookieContainer(uri);
            if (cookies != null)
            {
                foreach (Cookie cookie in cookies.GetCookies(uri))
                {
                    if (_cookies.ContainsKey(cookie.Name))
                    {
                        _cookies[cookie.Name] = cookie.Value;
                    }
                    else
                    {
                        _cookies.Add(cookie.Name, cookie.Value);
                    }
                }
            }
            cookies = GetUriCookieContainerEx(uri);
            if (cookies != null)
            {
                foreach (Cookie cookie in cookies.GetCookies(uri))
                {
                    if (_cookies.ContainsKey(cookie.Name))
                    {
                        _cookies[cookie.Name] = cookie.Value;
                    }
                    else
                    {
                        _cookies.Add(cookie.Name, cookie.Value);
                    }
                }
            }

        }

        public void Add(string cookie)
        {
            if (!string.IsNullOrEmpty(cookie))
            {
                string[] sarr1 = cookie.Split(';');
                foreach (string s in sarr1)
                {
                    string[] sarr2 = s.Trim().Split('=');
                    string key = "";
                    string value = "";
                    if (sarr2.Length >= 1)
                    {
                        key = sarr2[0];
                        if (sarr2.Length >= 2)
                        {
                            value = sarr2[1];
                        }
                        if (!_cookies.ContainsKey(key))
                        {
                            _cookies.Add(key, value);
                        }
                        else
                        {
                            _cookies[key] = value;
                        }
                    }
                }
            }
        }
        public Dictionary<string, string> Cookies
        {
            get
            {
                return _cookies;
            }
        }
        public string GetCookieString()
        {
            string s = "";
            string delim = "";
            foreach(string key in Cookies.Keys)
            {
                s += delim + key + Cookies[key];
                delim = "; ";
            }
            return s;
        }
        public string GetParamString()
        {
            string s = "";
            string delim = "";
            foreach (string key in Cookies.Keys)
            {
                s += delim + key + Cookies[key];
                delim = "&";
            }
            return s;
        }
    }
}
