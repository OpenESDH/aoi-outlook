using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEsdh.Outlook.Model.Configuration.Implementation
{
    public class OutlookConfiguration:System.Configuration.ConfigurationSection , Model.Configuration.Interface.IOutlookConfiguration
    {
        [System.Configuration.ConfigurationProperty("UploadEndPoint", DefaultValue = "https://alfresco.dk.vsw.datakraftverk.no:8443/alfresco/service/dk-openesdh-aoi-save", IsRequired = false)]
        public string UploadEndPoint
        {
            get
            {
                return (string)this["UploadEndPoint"];
            }
            set
            {
                this["UploadEndPoint"] = value;
            }
        }
        [System.Configuration.ConfigurationProperty("UseRedirectJavascript", DefaultValue = "true", IsRequired = false)]
        public bool UseRedirectJavascript
        {
            get
            {
                return (bool)this["UseRedirectJavascript"];
            }
            set
            {
                this["UseRedirectJavascript"] = value;
            }
        }
        [System.Configuration.ConfigurationProperty("MaxRedirectRetries", DefaultValue = "1", IsRequired = false)]
        public int MaxRedirectRetries
        {
            get
            {
                return (int)this["MaxRedirectRetries"];
            }
            set
            {
                this["MaxRedirectRetries"] = value;
            }
        }
        [System.Configuration.ConfigurationProperty("SaveAsDialogUrl", DefaultValue = "http://www.google.dk", IsRequired = false)]
        public string SaveAsDialogUrl
        {
            get
            {
                return (String)this["SaveAsDialogUrl"];
            }
            set
            {
                this["SaveAsDialogUrl"] = value;
            }
        }
        [System.Configuration.ConfigurationProperty("RecieveMessageClass", DefaultValue = "IPM.Note.OpenESDH", IsRequired = false)]
        public string RevieveMessageClass 
        {
            get
            {
                return (String)this["RecieveMessageClass"];
            }
            set
            {
                this["RecieveMessageClass"] = value;
            }
        }
        [System.Configuration.ConfigurationProperty("SendMessageClass", DefaultValue = "IPM.Note.OpenESDH", IsRequired = false)]
        public string SendMessageClass
        {
            get
            {
                return (String)this["SendMessageClass"];
            }
            set
            {
                this["SendMessageClass"] = value;
            }
        }
        [System.Configuration.ConfigurationProperty("PreAuthenticate", DefaultValue = "false", IsRequired = false)]
        public bool PreAuthenticate
        {
            get
            {
                return (this["PreAuthenticate"] != null ? bool.Parse(this["PreAuthenticate"].ToString()) : false);
            }
            set
            {
                this["PreAuthenticate"] = value;
            }
        }

        [System.Configuration.ConfigurationProperty("DialogExtend")]
        public ExtendDialog DialogExtend_Internal
        {
            get
            {
                try
                {
                    return (ExtendDialog)this["DialogExtend"];
                }catch(Exception ex)
                {
                    Logging.Logger.Current.LogException(ex);
                    throw ex;
                }
            }
            set
            {
                this["DialogExtend"] = value;
            }
        }
        public OpenEsdh.Outlook.Model.Configuration.Interface.IExtendDialog DialogExtend
        {
            get
            {
                return DialogExtend_Internal;
            }
            set
            {
                DialogExtend_Internal = new ExtendDialog() { X = value.X, Y = value.Y }; 
            }
        }
        [System.Configuration.ConfigurationProperty("CommunicationConfiguration",IsRequired=false)]
        public CommunicationConfiguration CommunicationConfiguration_Internal
        {
            get
            {
                try
                {
                    object o = this["CommunicationConfiguration"];
                    if (o != null)
                    {
                        return (CommunicationConfiguration)o;
                    }
                    else
                    {
                        return new CommunicationConfiguration();
                    }
                }catch(Exception ex)
                {
                    Logging.Logger.Current.LogException(ex);
                    throw ex;
                }
            
            }
            set
            {
                this["CommunicationConfiguration"] = value;
            }
        }
        [System.Configuration.ConfigurationProperty("DisplayRegion", IsRequired = false)]
        public DisplayRegionConfiguration DisplayRegion_internal
        {
            get
            {
                try
                {
                    object o = this["DisplayRegion"];
                    if (o != null)
                    {
                        return (DisplayRegionConfiguration)o;
                    }
                    else
                    {
                        return new DisplayRegionConfiguration();
                    }
                }
                catch (Exception ex)
                {
                    Logging.Logger.Current.LogException(ex);
                    throw ex;
                }

            }
            set
            {
                this["DisplayRegion"] = value;
            }

        }
        
        public Interface.IDisplayRegionConfiguration DisplayRegion
        {
            get
            {
                try
                {
                    return DisplayRegion_internal;
                }catch(Exception ex)
                {
                    Logging.Logger.Current.LogException(ex);
                    throw ex;
                }
            }

        }
        [System.Configuration.ConfigurationProperty("PreAuthentication", IsRequired = false)]
        public PreAuthenticationConfiguration PreAuthentication_internal
        {
            get
            {
                object o = this["PreAuthentication"];
                if (o != null)
                {
                    return (PreAuthenticationConfiguration)o;
                }
                else
                {
                    return new PreAuthenticationConfiguration();
                }
            }
            set
            {
                this["PreAuthentication"] = value;
            }
        }
        public Interface.IPreAuthenticateConfiguration PreAuthentication 
        {
            get
            {
                try
                {
                    return PreAuthentication_internal;
                }catch(Exception ex)
                {
                    Logging.Logger.Current.LogException(ex);
                    throw ex;
                }
            }
        }
        public Interface.ICommunicationConfiguration CommunicationConfiguration
        {
            get 
            {
                try
                {
                    return CommunicationConfiguration_Internal;
                }catch(Exception ex)
                {
                    Logging.Logger.Current.LogException(ex);
                    throw ex;
                }
            }
        }
        [System.Configuration.ConfigurationProperty("IgnoreCertificateErrors", DefaultValue = "false", IsRequired = false)]
        public bool IgnoreCertificateErrors
        {
            get { return bool.Parse(this["IgnoreCertificateErrors"] != null ? this["IgnoreCertificateErrors"].ToString() : "false"); }
            set { this["IgnoreCertificateErrors"] = value; }
        }
    }
    public class PreAuthenticationConfiguration:System.Configuration.ConfigurationElement,Model.Configuration.Interface.IPreAuthenticateConfiguration
    {
        public PreAuthenticationConfiguration()
        {

        }
        [System.Configuration.ConfigurationProperty("Username", DefaultValue = "", IsRequired = false)]
        public string Username
        {
            get { return (string)this["Username"]; }
            set { this["Username"] = value; }
        }

        [System.Configuration.ConfigurationProperty("Password", DefaultValue = "", IsRequired = false)]
        public string Password
        {
            get { return (string)this["Password"]; }
            set { this["Password"] = value; }
        }
        [System.Configuration.ConfigurationProperty("Domain", DefaultValue = "", IsRequired = false)]
        public string Domain 
        {
            get { return (string)this["Domain"]; }
            set { this["Domain"] = value; }
        }
        [System.Configuration.ConfigurationProperty("PreAuthenticateParameterName", DefaultValue = "alt_ticket", IsRequired = false)]
        public string PreAuthenticateParameterName
        {
            get { return (string)this["PreAuthenticateParameterName"]; }
            set { this["PreAuthenticateParameterName"] = value; }
        }

        [System.Configuration.ConfigurationProperty("UseConfigCredentials", DefaultValue = "false", IsRequired = false)]
        public bool UseConfigCredentials
        {
            get { return bool.Parse(this["UseConfigCredentials"] != null ? this["UseConfigCredentials"].ToString() : "false"); }
            set { this["UseConfigCredentials"] = value; }
        }

        [System.Configuration.ConfigurationProperty("AuthenticationUrl", DefaultValue = "", IsRequired = false)]
        public string AuthenticationUrl
        {
            get { return (string)this["AuthenticationUrl"]; }
            set { this["AuthenticationUrl"] = value; }
        }

        [System.Configuration.ConfigurationProperty("AuthenticationPackageFormat", DefaultValue = "AuthenticationPackageFormat", IsRequired = false)]
        public string AuthenticationPackageFormat
        {
            get { return (string)this["AuthenticationPackageFormat"]; }
            set { this["AuthenticationPackageFormat"] = value; }
        }
    }
    public class DisplayRegionConfiguration:System.Configuration.ConfigurationElement,Model.Configuration.Interface.IDisplayRegionConfiguration
    {
        [System.Configuration.ConfigurationProperty("DisplayDialogUrl", DefaultValue = "www.google.com", IsRequired = false)]
        public string DisplayDialogUrl
        {
            get { return (string)this["DisplayDialogUrl"]; }
            set { this["DisplayDialogUrl"] = value; }
        }

        [System.Configuration.ConfigurationProperty("RequestParameter", DefaultValue = "q", IsRequired = false)]
        public string RequestParameter
        {
            get { return (string)this["RequestParameter"]; }
            set { this["RequestParameter"] = value; }
        }
    }
    public class CommunicationConfiguration:System.Configuration.ConfigurationElement, Model.Configuration.Interface.ICommunicationConfiguration
    {
        public CommunicationConfiguration()
        {
            JavaScriptMethodName = "OpenESDHInitialize";
            PostMethodName = "Initialize";
            SendMethod_Internal = "GET";
            DelayUntilJavaMethodCall = 0;
        }
        [System.Configuration.ConfigurationProperty("DelayUntilJavaMethodCall", DefaultValue = "0", IsRequired = false)]
        public int DelayUntilJavaMethodCall 
        {
            get
            {
                return (int)this["DelayUntilJavaMethodCall"];
            }
            set
            {
                this["DelayUntilJavaMethodCall"] = value;
            }
        }
        [System.Configuration.ConfigurationProperty("JavaScriptMethodName", DefaultValue = "OpenESDHInitialize", IsRequired = false)]
        public string JavaScriptMethodName
        {
            get 
            {
                return (string)this["JavaScriptMethodName"];
            }
            set 
            {
                this["JavaScriptMethodName"] = value;
            }
        }
        [System.Configuration.ConfigurationProperty("PostMethodName", DefaultValue = "Initialize", IsRequired = false)]
        public string PostMethodName
        {
            get
            {
                return (string)this["PostMethodName"];
            }
            set
            {
                this["PostMethodName"] = value;
            }
        }
        [System.Configuration.ConfigurationProperty("SendMethod", DefaultValue = "GET", IsRequired = false)]
        public string SendMethod_Internal
        {
            get
            {
                return (string)this["SendMethod"];
            }
            set
            {
                this["SendMethod"] = value;
            }
        }
        public Interface.SendDataMethod SendMethod
        {
            get 
            { 
                switch(SendMethod_Internal.ToUpper())
                {
                    case "GET":
                        {
                            return Interface.SendDataMethod.JavascriptMethod;
                            
                        }
                    case "POST":
                        {
                            return Interface.SendDataMethod.Post;
                        }
                    default:
                        {
                            return Interface.SendDataMethod.JavascriptMethod;
                        }
                }
            }
            set 
            {
                switch(value)
                {
                    case Interface.SendDataMethod.JavascriptMethod:
                        {
                            SendMethod_Internal = "GET";
                            break;
                        }
                    case Interface.SendDataMethod.Post:
                        {
                            SendMethod_Internal = "POST";
                            break;
                        }
                    default:
                        {
                            SendMethod_Internal = "GET";
                            break;
                        }
                }
            }
        }
    }
    public class ExtendDialog:System.Configuration.ConfigurationElement, OpenEsdh.Outlook.Model.Configuration.Interface.IExtendDialog
    {
        [System.Configuration.ConfigurationProperty("X",DefaultValue="60",IsRequired=false)]
        public int X
        {
            get
            {
                return (int)this["X"];
            }
            set 
            {
                this["X"] = value;
            }
        }
        [System.Configuration.ConfigurationProperty("Y", DefaultValue = "10", IsRequired = false)]
        public int Y
        {
            get
            {
                return (int)this["Y"];
            }
            set
            {
                this["Y"] = value;
            }
        }
        [System.Configuration.ConfigurationProperty("MaxHeight", DefaultValue = "510", IsRequired = false)]
        public int MaxHeight
        {
            get
            {
                return (int)this["MaxHeight"];
            }
            set
            {
                this["MaxHeight"] = value;
            }
        }
        [System.Configuration.ConfigurationProperty("MaxWidth", DefaultValue = "280", IsRequired = false)]
        public int MaxWidth
        {
            get
            {
                return (int)this["MaxWidth"];
            }
            set
            {
                this["MaxWidth"] = value;
            }
        }

    }
    
}
