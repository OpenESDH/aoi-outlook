using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEsdh.Outlook.Presenters.Interface
{
    public delegate void SetMessageClassDelegate(string messageClass);
    public delegate void SetMessageIDDelegate(string ID);
    public delegate void UploadMailFileDelegate(string fullPathName,string name);

    public delegate void SaveMailBodyDelegate(UploadMailFileDelegate UploadFunction);
    public delegate void SaveAttachmentDelegate(string name, UploadMailFileDelegate UploadFunction);

    public interface ISaveAsPresenter
    {
        void Show(Model.EmailDescriptor Email);
        bool ShowAndSend(Model.EmailDescriptor Email);
        void Cancel();
        event SetMessageClassDelegate SetMessageClass;
        event SetMessageIDDelegate SetMessageID;
        event SaveMailBodyDelegate SaveMailBody;
        event SaveAttachmentDelegate SaveAttachment;
        void SaveAs(string unknown, Model.SelectableAttachment[] SelectedAttachments);
    }
}
