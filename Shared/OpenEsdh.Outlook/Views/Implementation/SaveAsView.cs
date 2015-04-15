using OpenEsdh.Outlook.Views.ServerCertificate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OpenEsdh.Outlook.Views.Implementation
{
    [ComVisible(true)]
    public partial class SaveAsView : Form,Views.Interface.ISaveAsBrowserView,Views.Interface.ISaveAsView
    {
        private Model.EmailDescriptor _Email = null;
        private Presenters.Interface.ISaveAsPresenter _presenter = null;
        private Model.Configuration.Interface.IOutlookConfiguration config = null;
        private string _startUrl = "";
        private int _redirectRetry = 0;
        public SaveAsView(Presenters.Interface.ISaveAsPresenter presenter):this()
        {
            _presenter = presenter;

        }
        public SaveAsView()
        {
            InitializeComponent();
            OpenEsdhBrowser.ObjectForScripting = this;
            this.Text = Model.Resources.ResourceResolver.Current.GetString("SaveAsDialogTitle");
            try
            {
                config = Model.Container.OutlookResolver.Current.Create<Model.Configuration.Interface.IOutlookConfiguration>();
                if(config.IgnoreCertificateErrors)
                {
                    // Hooked up on locale settings - so this will currently only work on English installations
                    WindowsInterop.SecurityAlertDialogWillBeShown +=
                        new GenericDelegate<Boolean, Boolean>(this.WindowsInterop_SecurityAlertDialogWillBeShown);
                }
            }catch(Exception ex)
            {
                Model.Logging.Logger.Current.LogException(ex);
            }
        }
        private Boolean WindowsInterop_SecurityAlertDialogWillBeShown(Boolean IsSSLDialog)
        {
            return true;
        }

        public void InitializeOpenEsdh(Model.Configuration.Interface.IOutlookConfiguration config, string jsonEmail)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(Initialize));
            t.Start(new object[]{jsonEmail});
        }
        public void Initialize(object o)
        {
            try
            {
                System.Threading.Thread.Sleep(config.CommunicationConfiguration.DelayUntilJavaMethodCall);
                this.Invoke((MethodInvoker)delegate
                    {
                        if (_startUrl.Contains(OpenEsdhBrowser.Url.AbsoluteUri))
                        {
                            object[] param = o as object[];
                            OpenEsdhBrowser.Document.InvokeScript(config.CommunicationConfiguration.JavaScriptMethodName, param);
                            OpenEsdhBrowser.DocumentCompleted -= DocumentCompleted;
                        }
                    });
            }catch(Exception ex)
            {
                Model.Logging.Logger.Current.LogException(ex);
            }
            
        }
        public void InitializeOpendEsdhPost(Model.Configuration.Interface.IOutlookConfiguration config, string payload)
        {
            //Nothing to do - data was sendt in postmessage
        }

        public void SaveAsOpenEsdh(string unknownJson, string attachmentSelectedJson)
        {  
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var selectedAttachments = serializer.Deserialize<Model.SelectableAttachment[]>(attachmentSelectedJson);
            this.SaveAs(unknownJson, selectedAttachments);
            this.Close();
            
        }

        public void CancelOpenEsdh()
        {
            this.Cancel();
        }
        private void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Model.Configuration.Interface.IOutlookConfiguration config = Model.Container.OutlookResolver.Current.Create<Model.Configuration.Interface.IOutlookConfiguration>();

            if (_redirectRetry<config.MaxRedirectRetries &&  !_startUrl.Contains(OpenEsdhBrowser.Url.AbsoluteUri))
            {
                _redirectRetry++;
		        if(!config.UseRedirectJavascript)
		        {
                	Initialize(_startUrl, _Email);
		        }else
		        {
                    OpenEsdhBrowser.Document.InvokeScript("eval",new object[]{ "document.location='" + _startUrl + "'"});

		        }		
                return;
            }
            _redirectRetry = 0;
            OpenEsdh.Outlook.Model.ServerCertificate.ICookieJar cookies = Model.Container.OutlookResolver.Current.Create<OpenEsdh.Outlook.Model.ServerCertificate.ICookieJar>();
            cookies.Add(OpenEsdhBrowser.Document.Cookie);
            cookies.AddCookiesForUri(OpenEsdhBrowser.Url);
            Rectangle r = OpenEsdhBrowser.Document.GetElementsByTagName("body")[0].OffsetRectangle;
            this.Height = Math.Max(this.Height, r.Height + config.DialogExtend.Y);
            this.Width = Math.Max(this.Width, r.Width + config.DialogExtend.X);
            this.Height = Math.Min(this.Height, config.DialogExtend.MaxHeight);
            this.Width = Math.Min(this.Width, config.DialogExtend.MaxWidth);

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var payload = serializer.Serialize(_Email);
            if (config.CommunicationConfiguration.SendMethod==Model.Configuration.Interface.SendDataMethod.JavascriptMethod)
            {
                this.InitializeOpenEsdh(config,payload);
            }
            else
            {
                this.InitializeOpendEsdhPost(config,payload);
            }
            
        }


        public void Initialize(string uri,Model.EmailDescriptor Email)
        {
            _startUrl = uri;
            _Email = Email;
            if(_redirectRetry==0)
            {
                OpenEsdhBrowser.DocumentCompleted += DocumentCompleted;            
            }
            if (config.CommunicationConfiguration.SendMethod == Model.Configuration.Interface.SendDataMethod.JavascriptMethod)
            {
                OpenEsdhBrowser.Url = new Uri(uri);
            }
            else
            {
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var payload = serializer.Serialize(_Email);
                payload = config.CommunicationConfiguration.PostMethodName + "=" + payload;
                
                OpenEsdhBrowser.Navigate(new Uri(uri), "_top", Encoding.ASCII.GetBytes(payload), "");
            }
            
        }

        public void SaveAs(string unknown, Model.SelectableAttachment[] SelectedAttachments)
        {
            try
            {
                Model.Logging.Logger.Current.LogInformation("SaveAs(" + unknown + "," + (SelectedAttachments!=null? SelectedAttachments.Length.ToString():"null") + ")");
                _presenter.SaveAs(unknown, SelectedAttachments);
            }catch(Exception ex)
            {
                Model.Logging.Logger.Current.LogException(ex);
                throw ex;
            }
        }

        public void Cancel()
        {
            _presenter.Cancel();
            this.Close();
        }
        public void ShowView()
        {
            this.ShowDialog();
        }
        public Presenters.Interface.ISaveAsPresenter Presenter 
        { 
            get
            {
                if(_presenter==null)
                {
                    _presenter = Model.Container.OutlookResolver.Current.Create<Presenters.Interface.ISaveAsPresenter>();
                }
                return _presenter;
            }

            set
            {
                _presenter = value;
            }
        }

        private void SaveAsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Cancel();
        }
    }
}
