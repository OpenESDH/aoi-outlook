using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace OpenEsdh.Outlook.Model.Resources
{
    public class ResourceResolver
    {
        private static ResourceResolver _current = null;
        private static object _lock = new object();
        public static ResourceResolver Current
        {
            get
            {
                if(_current==null)
                {
                    lock(_lock)
                    {
                        if(_current==null)
                        {
                            _current = new ResourceResolver();
                        }
                    }
                }
                return _current;
            }
        }
        private ResourceManager _mgr;
        public ResourceResolver()
        {
            _mgr = new ResourceManager("OpenEsdh.Outlook",this.GetType().Assembly);
        }
        
        public string GetString(string Key)
        {
            try
            {
                return OpenEsdh.Outlook.Resources.OpenEsdh_Outlook.ResourceManager.GetString(Key);
            }catch
            {
                return "(" + Key + ") Ikke Fundet";
            }
        }
    }
}
