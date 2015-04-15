using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEsdh.Outlook.Presenters.Implementation
{
    public class DisplayRegionPresenter:Interface.IDisplayRegionPresenter
    {
        private readonly Views.Interface.IDisplayRegion _displayRegion;
        private readonly Views.Interface.IDisplayRegionControl _displayRegionControl;
        private readonly Model.Configuration.Interface.IOutlookConfiguration _configuration;
        public DisplayRegionPresenter(Views.Interface.IDisplayRegion displayRegion)
        {
            _displayRegion = displayRegion;
            _displayRegionControl = Model.Container.OutlookResolver.Current.Create<Views.Interface.IDisplayRegionControl>();
            _configuration = Model.Container.OutlookResolver.Current.Create<Model.Configuration.Interface.IOutlookConfiguration>();
            if (_displayRegionControl is System.Windows.Forms.Control)
            {
                System.Windows.Forms.Control ctrl = _displayRegionControl as System.Windows.Forms.Control;
                
                DisplayRegion.FormControlCollection.Add(ctrl);
                ctrl.Dock = System.Windows.Forms.DockStyle.Fill;
            }
        }
        public Views.Interface.IDisplayRegion DisplayRegion
        {
            get { return _displayRegion; }
        }
        public void Show(Model.EmailDescriptor email)
        {
            string url = _configuration.DisplayRegion.DisplayDialogUrl;
            string delim = "?";
            var IDCol= (from metadata in email.MetaData
                        where metadata.Key == OpenEsdh.Outlook.Model.Configuration.Constants.IDColumn
                      select metadata);
            if(IDCol.Any())
            {
                url += delim+ _configuration.DisplayRegion.RequestParameter + "=" + System.Web.HttpUtility.UrlEncode(IDCol.First().Value);
                delim = "&";
            }
            _displayRegionControl.Show(url);
        }

    }
}
