using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PP.Pc
{
    public class TextBlock : Core.PcBase
    {
        public TextBlock()
        {
            InitializeComponent();
        }
        private string _text = "";
        public new string Text { get { return _text; } set { _text = value; this.Invalidate(); } }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            string t = string.IsNullOrEmpty(_text) ? "请输入文本内容" : _text;
            e.Graphics.DrawString(t, this.Font, Brushes.Black, new PointF(1, 1));
            base.OnPaint(e);
        }
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TextBlock
            // 
            this.Name = "TextBlock";
            this.Size = new System.Drawing.Size(114, 53);
            this.ResumeLayout(false);

        }
    }
}
