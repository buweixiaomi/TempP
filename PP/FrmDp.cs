﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            List<Core.PcBase> pcs = new List<Core.PcBase>();
            pcs.Add(new Core.PcBase());
            foreach (var a in pcs)
            {
                PictureBox pcx = new PictureBox();
                pcx.Location = new Point(10, 10);
                pcx.Width = 50;
                pcx.Height = 50;
                pcx.SizeMode = PictureBoxSizeMode.StretchImage;
                pcx.Image = a.Icon;
                panel1.Controls.Add(pcx);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}