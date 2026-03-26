using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Equant.SAV2000.ComponentLibrary.MVC.Components.DataTable
{
    public class DataTableSelector
    {
        public DataTableSelector()
        {
            this.Disabled = false;
            this.Checked = false;
            this.Hidden = false;
            this.Tooltip = null;
            this.HideAccess = null;
        }

        public bool Disabled {get; set;}
        public bool Checked { get; set; }
        public bool Hidden { get; set; }
        public string Tooltip { get; set; }
        public string HideAccess { get; set; }

        public bool Order { get { return this.Checked && !this.Hidden; } }
    }
}
