using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace PP.Core
{
    public class PcBase : UserControl, IIconable
    {
        public Point plocation { get; set; }
        public int zindex { get; set; }
        public bool designmode = true;
        public bool sizeauto = false;
        public FloatType floattype { get; set; }
        public Core.Border border { get; set; }
        public Core.Margin margin { get; set; }
        public Core.Margin padding { get; set; }

        List<Core.PcBase> ccontrols = new List<Core.PcBase>();


        private void Sort()
        {

            //left
            int line_left = 1;
            int width_left = 0;
            int height_left = 0;
            int beforehight_left = 0;
            //end left

            //right

            int line_right = 1;
            int width_right = 0;
            int height_right = 0;
            int beforehight_right = 0;
            //end right


            Size thisinnsersize = InnerSize;
            for (int k = 0; k < ccontrols.Count; k++)
            {
                Core.PcBase x = ccontrols[k];
                if (x.floattype == FloatType.Left)
                {
                    if (width_left != 0 && x.OuterSize.Width + width_left > thisinnsersize.Width)
                    {
                        line_left++;
                        beforehight_left += height_left;
                        width_left = 0;
                        height_left = 0;
                    }
                    x.Location = new System.Drawing.Point(x.OuterSize.Width - x.InnerSize.Width + width_left + (this.OuterSize.Width - thisinnsersize.Width), x.OuterSize.Height - x.InnerSize.Height + beforehight_left + (this.OuterSize.Height - thisinnsersize.Height));
                    width_left += x.OuterSize.Width;
                    if (x.OuterSize.Height > height_left)
                        height_left = x.OuterSize.Height;
                }
                else if (x.floattype == FloatType.Right)
                {
                    if (width_right != 0 && x.OuterSize.Width + width_right > thisinnsersize.Width)
                    {
                        line_right++;
                        beforehight_right += height_right;
                        width_right = 0;
                        height_right = 0;
                    }
                    x.Location = new System.Drawing.Point(this.OuterSize.Width - (this.padding.right + x.margin.right + x.border.right.width + x.Width + width_right), x.OuterSize.Height - x.InnerSize.Height + beforehight_right + (this.OuterSize.Height - thisinnsersize.Height));

                    width_right += x.OuterSize.Width;
                    if (x.OuterSize.Height > height_right)
                        height_right = x.OuterSize.Height;
                }
                else
                {
                    x.Location = new System.Drawing.Point(x.OuterSize.Width + (this.OuterSize.Width - thisinnsersize.Width), x.OuterSize.Height + (this.OuterSize.Height - thisinnsersize.Height));
                }

            }
        }


        public void AddC(PcBase c)
        {
            ccontrols.Add(c);
            Sort();
            this.Controls.Add(c);
        }

        public void AddRangeC(List<PcBase> cs)
        {
            ccontrols.AddRange(cs);
            Sort();
            this.Controls.AddRange(cs.ToArray());

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //e.Graphics.FillRectangle(Brushes.Red, this.ClientRectangle);
            PaintBorderDesign(e.Graphics);
            base.OnPaint(e);
        }

        private Size InnerSize
        {
            get
            {
                int width = this.Width - padding.left - padding.right;
                int height = this.Height - padding.top - padding.bottom;
                if (sizeauto && !designmode)
                {

                } return new System.Drawing.Size(width, height);
            }
        }

        public Size OuterSize
        {
            get
            {
                int width = this.Width + margin.left + margin.right + border.right.width + border.left.width;
                int height = this.Height;
                if (sizeauto && !designmode)
                {

                }
                return new System.Drawing.Size(width, height);

            }
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            Sort();
            base.OnLocationChanged(e);
        }
        protected void PaintBorderDesign(Graphics g)
        {
            Size s1 = OuterSize;
            Rectangle r1 = new System.Drawing.Rectangle(0, 0, s1.Width - 1, s1.Height - 1);
            //Rectangle r2 = new System.Drawing.Rectangle(1, 1, s1.Width - 1, s1.Height - 1);
            //Region rg1 = new System.Drawing.Region(r1);

            //Region rg2 = new System.Drawing.Region(r2);
            //rg1.Xor(rg2);
            //g.Clear(System.Drawing.Color.FromArgb(255, 255, 255, 255));
            g.DrawRectangle(Pens.Black, r1);
        }

        private Font _defautfont;
        public Font DefaultFont
        {
            get { return _defautfont; }
            set { _defautfont = value; }
        }
        private Image _icon;

        #region init
        private void InitIcon()
        {
            Bitmap bt = new Bitmap(50, 50);
            Graphics g = Graphics.FromImage(bt);
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255, 255)), 0, 0, bt.Width, bt.Height);
            //Rectangle r = new Rectangle(5, 43, 85, 15);
            //g.DrawRectangle(new Pen(Brushes.Black, 2), r);
            g.DrawLine(new Pen(Brushes.Black, 2f), new Point(5, 18), new Point(5, 35));
            g.DrawString("Abc", new Font(_defautfont.FontFamily, 20, FontStyle.Regular, GraphicsUnit.Pixel), Brushes.Black, new PointF(6, 17));
            g.Dispose();
            _icon = bt;
        }
        #endregion


        protected override void OnSizeChanged(EventArgs e)
        {
            Sort();
            base.OnSizeChanged(e);
        }

        public PcBase()
        {
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            _defautfont = SystemFonts.DefaultFont;
            InitIcon();
        }

        public Image Icon
        {
            get { return _icon.Clone() as Image; }
        }
    }
}
