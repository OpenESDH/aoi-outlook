using System;
using System.Collections.Generic;
namespace OpenEsdh.Outlook.Model.ServerCertificate
{
    interface ICookieJar
    {
        void Add(string cookie);
        Dictionary<string, string> Cookies { get; }
        void AddCookiesForUri(Uri uri);
        string GetCookieString();
        string GetParamString();
    }
}
