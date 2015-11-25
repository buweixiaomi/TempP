using System;
using System.Collections.Generic;
using System.Text;

namespace PP.Pc
{
    public class PPanel : Core.PcBase
    {
        public PPanel()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PPanel
            // 
            this.Name = "PPanel";
            this.Size = new System.Drawing.Size(440, 393);
            this.ResumeLayout(false);

        }
    }
}
