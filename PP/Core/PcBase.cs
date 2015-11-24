using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace PP.Core
{
    public class PcBase : UserControl, IIconable
    {
        public Point plocation { get; set; }

        public bool designmode = true;
        public bool sizeauto = false;
        public FloatType floattype { get; set; }
        public Core.Border border { get; set; }
        public Core.Margin margin { get; set; }
        public Core.Margin padding { get; set; }

        List<Core.PcBase> cfloatright = new List<Core.PcBase>();

        public void AddC(PcBase c)
        {
            cfloatright.Add(c);
            if(


        }

        public Size OuterSize
        {
            get
            {
                int width = this.Width + margin.left + margin.right + (border.right.style == Core.BorderStyle.None ? 0 : border.left.width) + (border.right.style == Core.BorderStyle.None ? 0 : border.right.width) + padding.left + padding.right;
                int height = this.Height;
                if (sizeauto && !designmode)
                {

                }
                return new System.Drawing.Size(width, height);

            }
        }

        protected void PaintBorderDesign(Graphics g)
        {
            Size s1 = OuterSize;
            Rectangle r1 = new System.Drawing.Rectangle(0, 0, s1.Width, s1.Height);
            //Rectangle r2 = new System.Drawing.Rectangle(1, 1, s1.Width - 1, s1.Height - 1);
            //Region rg1 = new System.Drawing.Region(r1);
            
            //Region rg2 = new System.Drawing.Region(r2);
            //rg1.Xor(rg2);

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

        public PcBase()
        {
            _defautfont = SystemFonts.DefaultFont;
            InitIcon();
        }

        public Image Icon
        {
            get { return _icon.Clone() as Image; }
        }
    }
}
