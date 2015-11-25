using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PP
{
    public partial class FrmDp : Form
    {
        public FrmDp()
        {
            InitializeComponent();
        }

        private void FrmDp_Load(object sender, EventArgs e)
        {

            Pc.PPanel p = new Pc.PPanel();
            p.Location = new Point(5, 5);
            p.Dock = DockStyle.Fill;
            this.Controls.Add(p);
         //   p.Click += c_MouseEnter;
            for (int i = 0; i < 2; i++)
            {
                var x = new Pc.TextBlock();
                x.floattype = Core.FloatType.Right;
                Random r = new Random();
                x.Width = r.Next(30, 400);
                Thread.Sleep(20);
                x.Height = r.Next(40, 200);
                p.AddC(x);
              //  p.Click += c_MouseEnter;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        PP.Core.MyRectControl resizemycontr = null;


        void c_MouseEnter(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (resizemycontr != null)
            {
                if (resizemycontr.currentControl == c)
                {
                    resizemycontr.Visible = true;
                    return;
                }
                c.Parent.Controls.Remove(resizemycontr);
                this.Controls.Remove(resizemycontr);
                resizemycontr = null;
            }
            resizemycontr = new PP.Core.MyRectControl(c);
            c.Parent.Controls.Add(resizemycontr);
        }
    }
}
