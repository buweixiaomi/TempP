using System;
using System.Collections.Generic;
using System.Text;

namespace PP.Pc
{
    public class RepateTemplate : Core.PcBase
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RepateTemplate
            // 
            this.Name = "RepateTemplate";
            this.Size = new System.Drawing.Size(261, 110);
            this.ResumeLayout(false);

        }

        private List<Core.PcBase> children = new List<Core.PcBase>();
        public RepateTemplate()
        {
            InitializeComponent();
        }

        public void AddC(Core.PcBase c)
        {
            children.Add(c);
            this.Controls.Add(c);



        }



        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            SiteHeight();
        }

        private void SiteHeight()
        {
            int maxheight = this.Height;
            if (sizeauto && !designmode)
            {
                foreach (var a in children)
                {
                    if (a.OuterSize.Height > maxheight)
                        maxheight = a.OuterSize.Height;
                }
                this.Height = maxheight;
            }

        }



    }
}
