using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PP.Core
{
    public partial class MyRectControl : UserControl
    {
        public Control currentControl;
        private Size Square = new Size(6, 6);
        // private Size InnerSize = new Size(2, 2);

        private Rectangle baseRect = new Rectangle();
        private Rectangle[] SmallRect = new Rectangle[8];
        private Rectangle[] BoundRect = new Rectangle[4];
        //  private Graphics g;
        public Rectangle Rect
        {
            get { return baseRect; }
            set
            {
                int X = Square.Width;
                int Y = Square.Height;
                int Height = value.Height;
                int Width = value.Width;
                baseRect = new Rectangle(X, Y, Width, Height);
                SetRectangles();
            }
        }

        public MyRectControl(Control theControl)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.Selectable, true);
            Application.AddMessageFilter(new MyMoveMessage());
            currentControl = theControl;
            Create();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }
        private void Create()
        {
            //创建边界
            int X = currentControl.Bounds.X - Square.Width;
            int Y = currentControl.Bounds.Y - Square.Height;
            int Height = currentControl.Bounds.Height + (Square.Height * 2);
            int Width = currentControl.Bounds.Width + (Square.Width * 2);
            this.Bounds = new Rectangle(X, Y, Width, Height);
            Rect = currentControl.Bounds;
            //设置可视区域
            this.Region = new Region(BuildFrame());
            // g = this.CreateGraphics();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e.Graphics);
            MakeLine();
            this.Focus();
        }



        List<MoveLine> willlines = new List<MoveLine>();
        private void MakeLine()
        {
            //for (int i = 0; i < willlines.Count; i++)
            //{
            //    this.Parent.Controls.Remove(willlines[i]);
            //}
            if (!size_or_move_action)
            {
                for (int i = 0; i < willlines.Count; i++)
                {
                    this.Parent.Controls.Remove(willlines[i]);
                }
                willlines.Clear();
                return;
            }

            Control pc = this.Parent;
            if (pc == null)
                pc = this.ParentForm;

            List<Point> ps = new List<Point>();
            foreach (Control c in pc.Controls)
            {
                if (c == currentControl || c == this)
                    continue;
                var tr = c.Bounds;
                ps.Add(new Point(tr.X, tr.Y));
                ps.Add(new Point(tr.X + tr.Width, tr.Y + tr.Height));
                ps.Add(new Point(tr.X + tr.Width, tr.Y));
                ps.Add(new Point(tr.X, tr.Y + tr.Height));
            }
            List<Point> thispoint = new List<Point>();
            var thisrec = currentControl.Bounds;
            thispoint.Add(new Point(thisrec.X, thisrec.Y));
            thispoint.Add(new Point(thisrec.X, thisrec.Y + thisrec.Height));
            thispoint.Add(new Point(thisrec.X + thisrec.Width, thisrec.Y));
            thispoint.Add(new Point(thisrec.X + thisrec.Width, thisrec.Y + thisrec.Height));
            List<MoveLine> newlist = new List<MoveLine>();
            foreach (var currp in thispoint)
            {
                foreach (var othrep in ps)
                {
                    if (currp.X == othrep.X && currp.Y == othrep.Y)
                        continue;
                    if (currp.X == othrep.X)
                    {
                        newlist.Add(new MoveLine(currp, othrep));
                    }
                    else if (currp.Y == othrep.Y)
                    {
                        newlist.Add(new MoveLine(currp, othrep));
                    }
                }
            }
            List<MoveLine> remove = new List<MoveLine>();
            List<MoveLine> beforeadd = new List<MoveLine>();
            foreach (var a in newlist)
            {
                foreach (var b in willlines)
                {

                    if (a.ISSameLine(b.point1, b.point2))
                    {
                        remove.Add(a);
                        beforeadd.Add(b);
                    }
                }
            }
            foreach (var a in remove)
            {
                newlist.Remove(a);
            }

            foreach (var a in willlines)
            {
                if (!beforeadd.Contains(a))
                    this.Parent.Controls.Remove(a);
            }
            foreach (var a in newlist)
                this.Parent.Controls.Add(a);
            willlines.Clear();
            willlines.AddRange(newlist);
            willlines.AddRange(beforeadd);


        }


        private GraphicsPath BuildFrame()
        {
            GraphicsPath path = new GraphicsPath();
            BoundRect[0] = new Rectangle(0, 0, currentControl.Bounds.Width + (Square.Width * 2), Square.Height);
            BoundRect[1] = new Rectangle(BoundRect[0].X, currentControl.Bounds.Height + Square.Height, BoundRect[0].Width, BoundRect[0].Height);

            BoundRect[2] = new Rectangle(0, Square.Height, Square.Width, currentControl.Bounds.Height);
            BoundRect[3] = new Rectangle(currentControl.Bounds.Width + Square.Width, BoundRect[2].Y, BoundRect[2].Width, BoundRect[2].Height);
            path.AddRectangle(BoundRect[0]);
            path.AddRectangle(BoundRect[1]);
            path.AddRectangle(BoundRect[2]);
            path.AddRectangle(BoundRect[3]);
            return path;
        }


        public void SetRectangles()
        {
            Size tmpSquare = new System.Drawing.Size(Square.Width - 1, Square.Height - 1);
            //定义8个小正方形的范围
            //左上
            SmallRect[0] = new Rectangle(new Point(baseRect.X - Square.Width, baseRect.Y - Square.Height), tmpSquare);
            //上中间
            SmallRect[1] = new Rectangle(new Point(baseRect.X + (baseRect.Width / 2) - (Square.Width / 2), baseRect.Y - Square.Height), tmpSquare);
            //右上
            SmallRect[2] = new Rectangle(new Point(baseRect.X + baseRect.Width, baseRect.Y - Square.Height), tmpSquare);


            //右中间
            SmallRect[3] = new Rectangle(new Point(baseRect.X + baseRect.Width, baseRect.Y + (baseRect.Height / 2) - (Square.Height / 2)), tmpSquare);
            //右下
            SmallRect[4] = new Rectangle(new Point(baseRect.X + baseRect.Width, baseRect.Y + baseRect.Height), tmpSquare);
            //下中间
            SmallRect[5] = new Rectangle(new Point(baseRect.X + (baseRect.Width / 2) - (Square.Width / 2), baseRect.Y + baseRect.Height), tmpSquare);
            //左下
            SmallRect[6] = new Rectangle(new Point(baseRect.X - Square.Width, baseRect.Y + baseRect.Height), tmpSquare);
            //左中间
            SmallRect[7] = new Rectangle(new Point(baseRect.X - Square.Width, baseRect.Y + (baseRect.Height / 2) - (Square.Height / 2)), tmpSquare);
            //整个包括周围边框的范围
            //   ControlRect = new Rectangle(new Point(0, 0), this.Bounds.Size);
        }

        //边框和锚点的大小位置计算完毕后，我们开始实际的绘制操作：
        public void Draw(Graphics g)
        {
            try
            {
                this.BringToFront();
                g.FillRectangles(Brushes.LightGray, BoundRect); //填充用于调整的边框的内部
                g.FillRectangles(Brushes.White, SmallRect); //填充8个锚点的内部
                g.DrawRectangles(Pens.Black, SmallRect); //绘制8个锚点的黑色边线

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private Cursor GetMouthType(HitDownSquare i)
        {
            Cursor tempcursor = System.Windows.Forms.Cursors.Default;
            switch (i)
            {
                case HitDownSquare.HDS_WN:
                    tempcursor = System.Windows.Forms.Cursors.SizeNWSE;
                    break;
                case HitDownSquare.HDS_N:
                    tempcursor = System.Windows.Forms.Cursors.SizeNS;
                    break;
                case HitDownSquare.HDS_EN:
                    tempcursor = System.Windows.Forms.Cursors.SizeNESW;
                    break;
                case HitDownSquare.HDS_E:
                    tempcursor = System.Windows.Forms.Cursors.SizeWE;
                    break;
                case HitDownSquare.HDS_ES:
                    tempcursor = System.Windows.Forms.Cursors.SizeNWSE;
                    break;
                case HitDownSquare.HDS_S:
                    tempcursor = System.Windows.Forms.Cursors.SizeNS;
                    break;
                case HitDownSquare.HDS_WS:
                    tempcursor = System.Windows.Forms.Cursors.SizeNESW;
                    break;
                case HitDownSquare.HDS_W:
                    tempcursor = System.Windows.Forms.Cursors.SizeWE;
                    break;
                case HitDownSquare.HDS_INNER:
                    tempcursor = System.Windows.Forms.Cursors.SizeAll;
                    break;
                default:
                    break;
            }
            return tempcursor;
        }

        private HitDownSquare currmousepoint = HitDownSquare.HDS_NONE;
        private HitDownSquare mousedownpoint = HitDownSquare.HDS_NONE;
        private void MyRectControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                ResetMove();
            }
            currmousepoint = GetPositionOfMouse(e.X, e.Y);
            if (!size_or_move_action)
            {
                this.Cursor = GetMouthType(currmousepoint);
            }

            this.ParentForm.Text = string.Format("({0},{1})", e.X, e.Y);

            if (size_or_move_action)
            {
                if (e.X == mouse_point.X && e.Y == mouse_point.Y)
                {
                    return;
                }
                ResizeOrMove(e.X, e.Y);
                mouse_point = new Point(e.X, e.Y);
                MakeLine();
            }

        }

        private void ResizeOrMove(int x, int y)
        {
            int changex = x - mouse_point.X;
            int changey = y - mouse_point.Y;
            switch (mousedownpoint)
            {
                case HitDownSquare.HDS_NONE:
                    break;
                case HitDownSquare.HDS_WN:
                    this.Visible = false;
                    currentControl.Size = new System.Drawing.Size(currentControl.Size.Width - changex, currentControl.Size.Height - changey);
                    currentControl.Location = new Point(currentControl.Location.X + changex, currentControl.Location.Y + changey);
                    break;
                case HitDownSquare.HDS_N:
                    this.Visible = false;
                    currentControl.Size = new System.Drawing.Size(currentControl.Size.Width, currentControl.Size.Height - changey);
                    currentControl.Location = new Point(currentControl.Location.X, currentControl.Location.Y + changey);
                    // mouse_point = new Point(x, y);
                    break;
                case HitDownSquare.HDS_EN:
                    this.Visible = false;
                    currentControl.Size = new System.Drawing.Size(currentControl.Size.Width + changex, currentControl.Size.Height - changey);
                    currentControl.Location = new Point(currentControl.Location.X, currentControl.Location.Y + changey);
                    // mouse_point = new Point(x, y);
                    break;
                case HitDownSquare.HDS_E:
                    this.Visible = false;
                    currentControl.Size = new System.Drawing.Size(currentControl.Size.Width + changex, currentControl.Size.Height);
                    currentControl.Location = new Point(currentControl.Location.X, currentControl.Location.Y);
                    // mouse_point = new Point(x, y);
                    break;
                case HitDownSquare.HDS_ES:
                    this.Visible = false;
                    currentControl.Size = new System.Drawing.Size(currentControl.Size.Width + changex, currentControl.Size.Height + changey);
                    currentControl.Location = new Point(currentControl.Location.X, currentControl.Location.Y);
                    //  mouse_point = new Point(x, y);
                    break;
                case HitDownSquare.HDS_S:
                    this.Visible = false;
                    currentControl.Size = new System.Drawing.Size(currentControl.Size.Width, currentControl.Size.Height + changey);
                    currentControl.Location = new Point(currentControl.Location.X, currentControl.Location.Y);
                    // mouse_point = new Point(x, y);
                    break;
                case HitDownSquare.HDS_WS:
                    this.Visible = false;
                    currentControl.Size = new System.Drawing.Size(currentControl.Size.Width - changex, currentControl.Size.Height + changey);
                    currentControl.Location = new Point(currentControl.Location.X + changex, currentControl.Location.Y);
                    // mouse_point = new Point(x, y);
                    break;
                case HitDownSquare.HDS_W:
                    this.Visible = false;
                    currentControl.Size = new System.Drawing.Size(currentControl.Size.Width - changex, currentControl.Size.Height);
                    currentControl.Location = new Point(currentControl.Location.X + changex, currentControl.Location.Y);
                    //   mouse_point = new Point(x, y);
                    break;
                case HitDownSquare.HDS_INNER:
                    this.Visible = false;
                    currentControl.Location = new Point(currentControl.Location.X + changex, currentControl.Location.Y + changey);
                    // mouse_point = new Point(x, y);
                    break;
                default:
                    break;
            }
        }


        private HitDownSquare GetPositionOfMouse(int x, int y)
        {
            int mindex = 8;
            for (int i = 0; i < SmallRect.Length; i++)
            {
                bool containpoint = SmallRect[i].Contains(x, y);
                if (containpoint)
                {
                    mindex = i;
                    break;
                }
            }
            var tposition = GetPointPosition(mindex);
            return tposition;
        }

        private HitDownSquare GetPointPosition(int index)
        {
            index++;
            if (index > 0 && index < 10)
            {
                return (HitDownSquare)index;
            }
            return HitDownSquare.HDS_NONE;
        }

        private bool size_or_move_action = false;
        private Point mouse_point = new Point(0, 0);
        private void MyRectControl_MouseUp(object sender, MouseEventArgs e)
        {
            ResetMove();
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    currentControl.Location = new Point(currentControl.Location.X, currentControl.Location.Y - 1);
                    Create();
                    this.Invalidate();
                    break;
                case Keys.Right:
                    currentControl.Location = new Point(currentControl.Location.X + 1, currentControl.Location.Y);
                    Create();
                    this.Invalidate();
                    break;
                case Keys.Down:
                    currentControl.Location = new Point(currentControl.Location.X, currentControl.Location.Y + 1);
                    Create();
                    this.Invalidate();
                    break;
                case Keys.Left:
                    currentControl.Location = new Point(currentControl.Location.X - 1, currentControl.Location.Y);
                    Create();
                    this.Invalidate();
                    break;
                default:
                    break;
            }
        }
        private void ResetMove()
        {
            if (size_or_move_action)
            {
                Create();
                this.Visible = true;
                size_or_move_action = false;
            }
            mousedownpoint = HitDownSquare.HDS_NONE;
            mouse_point = new Point(0, 0);
            MakeLine();
        }

        private void MyRectControl_MouseDown(object sender, MouseEventArgs e)
        {
            ResetMove();
            mousedownpoint = GetPositionOfMouse(e.X, e.Y);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (mousedownpoint == HitDownSquare.HDS_NONE)
                {
                    size_or_move_action = false;
                    mouse_point = new Point(0, 0);
                }
                else
                {
                    size_or_move_action = true;
                    mouse_point = new Point(e.X, e.Y);
                    MakeLine();
                    this.Focus();
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x105 || m.Msg == 0x100 || m.Msg == 0x101 || m.Msg == 0x102 || m.Msg == 0x260)
            {
                KeyEventArgs e = new KeyEventArgs(((Keys)((int)((long)m.WParam))) | ModifierKeys);
                this.OnKeyDown(e);
                return;
            }
            base.WndProc(ref m);
        }
    }

    enum HitDownSquare
    {
        HDS_NONE = 0,
        HDS_WN = 1,
        HDS_N = 2,
        HDS_EN = 3,
        HDS_E = 4,
        HDS_ES = 5,
        HDS_S = 6,
        HDS_WS = 7,
        HDS_W = 8,
        HDS_INNER = 9
    }

    public class MyMoveMessage : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x105)
            {
                return false;
            }
            return false;
        }
    }


}
