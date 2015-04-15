using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace OpenEsdh.Outlook.Presenters.Implementation
{
    public class SaveAsPresenter:Presenters.Interface.ISaveAsPresenter
    {
        private readonly Views.Interface.ISaveAsView _view;
        private readonly Model.Configuration.Interface.IOutlookConfiguration _configuration;
        private bool _result = false;
        private bool _inOperation = false;
        public event OpenEsdh.Outlook.Presenters.Interface.SetMessageIDDelegate SetMessageID;
        public event OpenEsdh.Outlook.Presenters.Interface.SetMessageClassDelegate SetMessageClass;
        public event OpenEsdh.Outlook.Presenters.Interface.SaveMailBodyDelegate SaveMailBody;
        public event OpenEsdh.Outlook.Presenters.Interface.SaveAttachmentDelegate SaveAttachment;

        public SaveAsPresenter(Views.Interface.ISaveAsView view)
        {
            _view = view;
            _configuration = Model.Container.OutlookResolver.Current.Create<Model.Configuration.Interface.IOutlookConfiguration>();
        }
        public SaveAsPresenter()
            : this(Model.Container.OutlookResolver.Current.Create<Views.Interface.ISaveAsView>())
        {

        }
        private void DoSetMessageId(string messageId)
        {
            if(SetMessageID!=null)
            {
                SetMessageID(messageId);
            }
        }
        private void DoSetMessageClass(string messageClass)
        {
            if(SetMessageClass!=null)
            {
                SetMessageClass(messageClass);
            }
        }
        public void Cancel()
        {
            if(_inOperation)
            {
                return;
            }
            try
            {
                _inOperation = true;
                _result = false;
                _view.Cancel();
            }finally
            {
                _inOperation = false;
            }
        }
        public bool ShowAndSend(Model.EmailDescriptor Email)
        {
            _result = false;
            Show(Email);
            return _result;
        }
        public void Show(Model.EmailDescriptor Email)
        {
            try
            {
                if(_configuration==null)
                {
                    Model.Logging.Logger.Current.LogInformation("Configuration is null");
                }
                if(string.IsNullOrEmpty(_configuration.SaveAsDialogUrl))
                {
                    Model.Logging.Logger.Current.LogInformation("SaveAsDialogUrl is null");
                }
                if(Email==null)
                {
                    Model.Logging.Logger.Current.LogInformation("Email is null");
                }
                if(_view ==null)
                {
                    Model.Logging.Logger.Current.LogInformation("View is null");
                }
                string url = _configuration.SaveAsDialogUrl;
                _view.Initialize(url, Email);
                _view.ShowView();
            }catch(Exception ex)
            {
                Model.Logging.Logger.Current.LogException(ex);
                throw ex;
            }
        }



        public void SaveAs(string unknown, Model.SelectableAttachment[] SelectedAttachments)
        {
            ///TODO: Set to async operation with progressbar
            try
            {
                Model.Alfresco.IAlfrescoFilePost Upload = Outlook.Model.Container.OutlookResolver.Current.Create<Model.Alfresco.IAlfrescoFilePost>();
                if (SaveMailBody != null)
                {
                    SaveMailBody((filename,name) =>
                        {
                            try
                            {
                                Upload.UploadFile(unknown, filename,name);
                            }
                            catch (Exception ex)
                            {
                                Model.Logging.Logger.Current.LogException(ex);
                                throw ex;
                            }
                        });
                }
                if (SaveAttachment != null)
                {
                    foreach (var att in SelectedAttachments)
                    {
                        SaveAttachment(att.Name, (filename,name) =>
                        {
                            try
                            {
                                Upload.UploadFile(unknown, filename,name);
                            }
                            catch (Exception ex)
                            {
                                Model.Logging.Logger.Current.LogException(ex);
                                throw ex;
                            }
                        });
                    }
                }
                try
                {
                    var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    UploadTicket ticket = serializer.Deserialize<UploadTicket>(unknown);
                    SetMessageClass(_configuration.RevieveMessageClass);
                    SetMessageID(ticket.nodeRef);
                }catch(Exception ex)
                {
                    Model.Logging.Logger.Current.LogException(ex);
                }
                
            }catch(Exception ex)
            {
                Model.Logging.Logger.Current.LogException(ex);
            }
        }
    }

    public class UploadTicket
    {
        public string nodeRef { get; set; } 
    }

}
