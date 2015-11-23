using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PP.Core
{
    public class PcBase : IIconable
    {

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
