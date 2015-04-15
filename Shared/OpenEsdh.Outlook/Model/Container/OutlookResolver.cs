using OpenEsdh.Outlook.Views.ServerCertificate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Text;

namespace OpenEsdh.Outlook.Model.Container
{
    public class OutlookResolver:TypeResolver,IDisposable
    {
        private static bool CertificateAccepterInitialized = false;
        
        private static TypeResolver _current = null;
        private static object _lock = new object();
        private Type _configurationFileOwner = null;
        
        public OutlookResolver(Type ConfigurationFileOwner)
        {
            _configurationFileOwner = ConfigurationFileOwner;
        }
        public static TypeResolver Current
        {
            get
            {
                return _current;
            }
            set
            {
                lock (_lock)
                {
                    _current = value;
                }
            }
        }

        protected override void BuildComponents()
        {
            base.AddComponent<Model.Configuration.Interface.IOutlookConfiguration>(() => 
            {
                try
                {
                    if (_singletons.ContainsKey(typeof(Model.Configuration.Interface.IOutlookConfiguration)))
                    {
                        return _singletons[typeof(Model.Configuration.Interface.IOutlookConfiguration)];
                    }

                    Assembly assembly = System.Reflection.Assembly.GetAssembly(_configurationFileOwner);
                    string localPath = new Uri(assembly.CodeBase).LocalPath;
                    System.Configuration.Configuration conf= System.Configuration.ConfigurationManager.OpenExeConfiguration(localPath);

                    var config= (Model.Configuration.Implementation.OutlookConfiguration)conf.GetSection("Outlook");
                    _singletons.Add(typeof(Model.Configuration.Interface.IOutlookConfiguration), config);
                    if (config.IgnoreCertificateErrors && !CertificateAccepterInitialized)
                    {
                        WindowsInterop.Hook();
                        ServicePointManager.ServerCertificateValidationCallback =
                                        new RemoteCertificateValidationCallback(
                                            delegate
                                            { return true; }
                                        );
                        CertificateAccepterInitialized = true;
                    }

                    return config;

                }catch(Exception ex)
                {
                    return new Model.Configuration.Implementation.OutlookConfiguration(); 
                }
                
                
            });
            base.AddComponent<Model.IPreAuthenticator>(() =>
                {
                    if(_singletons.ContainsKey(typeof(Model.IPreAuthenticator)))
                    {
                        return _singletons[typeof(Model.IPreAuthenticator)];
                    }
                    var conf = this.Create<Model.Configuration.Interface.IOutlookConfiguration>();
                    Model.PreAuthenticator authenticator = new PreAuthenticator(conf);
                    _singletons.Add(typeof(Model.IPreAuthenticator), authenticator);
                    return authenticator;
                });
            base.AddComponent<Views.Interface.ISaveAsView>(() => { return new Views.Implementation.SaveAsView(); });
            base.AddComponent<Presenters.Interface.ISaveAsPresenter>(() => 
            {
                var view = this.Create<Views.Interface.ISaveAsView>();
                var presenter= new Presenters.Implementation.SaveAsPresenter(view);
                view.Presenter = presenter;
                return presenter;
            });
            base.AddComponent<Views.Interface.IDisplayRegionControl>(() =>
                {
                    var displayRegion = new Views.Implementation.DisplayRegionControl();
                    return displayRegion;
                });
            base.AddComponentWithParam<Presenters.Interface.IDisplayRegionPresenter>((inputParam) =>
                {
                    var presenter = new Presenters.Implementation.DisplayRegionPresenter(inputParam as Views.Interface.IDisplayRegion);
                    return presenter;
                });
            base.AddComponent<Model.Alfresco.IAlfrescoFilePost>(() =>
            {
                var conf = this.Create<Model.Configuration.Interface.IOutlookConfiguration>();
                Model.Alfresco.AlfrescoFilePost filePost = new Alfresco.AlfrescoFilePost(conf.UploadEndPoint);
                return filePost;
            });
            base.AddComponent<Model.ServerCertificate.ICookieJar>(() =>
            {
                if (_singletons.ContainsKey(typeof(Model.ServerCertificate.ICookieJar)))
                {
                    return _singletons[typeof(Model.ServerCertificate.ICookieJar)];
                }
                var CookieJar = new Model.ServerCertificate.CookieJar();
                _singletons.Add(typeof(Model.ServerCertificate.ICookieJar), CookieJar);
                return CookieJar;
            });
        }

        public void Dispose()
        {
            if(CertificateAccepterInitialized)
            { 
                WindowsInterop.Unhook();
                CertificateAccepterInitialized = false;
            }
        }
    }
}
