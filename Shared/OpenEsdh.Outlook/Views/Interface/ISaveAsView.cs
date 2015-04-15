using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEsdh.Outlook.Views.Interface
{
    public interface ISaveAsBrowserView
    {
        void InitializeOpenEsdh(Model.Configuration.Interface.IOutlookConfiguration config,string jsonEmail);
        void InitializeOpendEsdhPost(Model.Configuration.Interface.IOutlookConfiguration config, string payload);
        void SaveAsOpenEsdh(string unknownJson, string attachmentSelectedJson);
        void CancelOpenEsdh();
    }
    public interface ISaveAsView
    {
        void Initialize(string uri, Model.EmailDescriptor Email);
        void SaveAs(string unknown, Model.SelectableAttachment[] SelectedAttachments);
        void Cancel();
        void ShowView();
        Presenters.Interface.ISaveAsPresenter Presenter { get; set; }
    }

}
