using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEsdh.Outlook.Model.Configuration.Interface
{
    public interface IOutlookConfiguration
    {
        string UploadEndPoint { get; }
        string SaveAsDialogUrl { get; }
        string RevieveMessageClass { get; }
        string SendMessageClass { get; }
        bool PreAuthenticate { get; }
        bool IgnoreCertificateErrors { get; }
        int MaxRedirectRetries { get; }
        bool UseRedirectJavascript { get; }
        IExtendDialog DialogExtend { get; }
        ICommunicationConfiguration CommunicationConfiguration{get;}
        IDisplayRegionConfiguration DisplayRegion { get; }
        IPreAuthenticateConfiguration PreAuthentication { get; }
    }
    public enum SendDataMethod
    {
        JavascriptMethod,
        Post
    }

    public interface ICommunicationConfiguration
    {
        int DelayUntilJavaMethodCall { get; }
        SendDataMethod SendMethod { get; }
        string JavaScriptMethodName { get; }
        string PostMethodName { get; }
    }
    public interface IExtendDialog
    {
        int X { get; }
        int Y { get; }
        int MaxWidth { get; }
        int MaxHeight { get; }
    }
    public interface IDisplayRegionConfiguration
    {
        string DisplayDialogUrl { get; }
        string RequestParameter{get;}
    }
    public interface IPreAuthenticateConfiguration
    {
        string Username { get; }
        string Password { get; }
        string Domain { get; }
        string PreAuthenticateParameterName { get; }
        bool UseConfigCredentials { get; }
        string AuthenticationUrl { get; }
        string AuthenticationPackageFormat { get; }
    }
}
