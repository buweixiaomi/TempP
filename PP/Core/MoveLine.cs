using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace PP.Core
{
    public partial class MoveLine : UserControl
    {

        public Point point1 { get; set; }
        public Point point2 { get; set; }
        public Rectangle RegionRect;
        public MoveLine(Point p1, Point p2)
        {
            InitializeComponent();
            point1 = p1;
            point2 = p2;
            this.Bounds = new Rectangle(0, 0, 1, 1);
            if (point1.X == point2.X)
            {
                RegionRect = new Rectangle(point1.X, Math.Min(point1.Y, point2.Y), 1, Math.Abs(point1.Y - point2.Y));
            }
            else if (point1.Y == point2.Y)
            {
                RegionRect = new Rectangle(Math.Min(point1.X, point2.X), point1.Y, Math.Abs(point1.X - point2.X), 1);
            }
            this.Region = new Region(RegionRect);
        }

        public bool Update(Point p1, Point p2)
        {
            if ( p1.X == p2.X&&RegionRect.Height==1&&p1.X==RegionRect.X)
            {
                RegionRect = new Rectangle(RegionRect.X,Math.Min(RegionRect.Y, Math.Min(p1.Y, p2.Y)), RegionRect.Width, Math.Max(Math.Abs(p1.Y - p2.Y),RegionRect.Height));
                return true;
            }
            else if (p1.Y == p2.Y)
            {
                RegionRect = new Rectangle(Math.Min(RegionRect.X, Math.Min(p1.X, p2.X)),RegionRect.Y, Math.Max(Math.Abs(p1.X - p2.X),RegionRect.Width), RegionRect.Height);
              //  tr = new Rectangle(Math.Min(p1.X, p2.X), p1.Y, Math.Abs(p1.X - p2.X), 1);
                return true;
            }
            return false;
        }




        protected override void OnPaint(PaintEventArgs e)
        {

            this.Bounds = new Rectangle(0, 0, this.Parent.Width, this.Parent.Height);

            this.BringToFront();
            e.Graphics.DrawLine(Pens.Red, point1, point2);
            //e.Graphics.FillRegion(Brushes.Red, this.Region);
        }

        public bool ISSameLine(Point p1, Point p2)
        {
            if (p1.X == point1.X && p1.Y == point1.Y && p2.X == point2.X && p2.Y == point2.Y)
            {
                return true;
            }

            if (p1.X == point2.X && p1.Y == point2.Y && p2.X == point1.X && p2.Y == point1.Y)
            {
                return true;
            }
            return false;
        }

    }
}
