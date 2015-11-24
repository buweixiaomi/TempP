using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PP.Pc
{
    public class TableGrid : Core.PcBase
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
        }
    }

    public class TableGridColumn
    {
        public string bindname { get; set; }
        public string caption { get; set; }
    }

    public class TableRowTemplate
    { 
        
    }
}
