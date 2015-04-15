using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenEsdh.Outlook.Presenters.Interface
{
    public interface IDisplayRegionPresenter
    {
        Views.Interface.IDisplayRegion DisplayRegion { get; }
        void Show(Model.EmailDescriptor email);
    }
}
