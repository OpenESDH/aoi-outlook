using System;
namespace OpenEsdh.Outlook.Model.Alfresco
{
    public interface IAlfrescoFilePost
    {
        string UploadFile(string address, string unknownJson, string fileName,string name);
        string UploadFile(string unknownJson, string fileName,string name);
    }
}
