using System;
namespace OpenEsdh._2010.Outlook.Presentation.Interface
{
    interface ISaveEmailPresenter
    {
        ISaveEmailButtonView View { get; set; }
        void SaveEmailClick(dynamic Context);
        bool SaveEmailAndSend(dynamic Context);
        void Load(dynamic Context);
    }
}
