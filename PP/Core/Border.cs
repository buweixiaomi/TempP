using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace PP.Core
{
    public struct Border
    {
        public BorderItem top { get; set; }
        public BorderItem right { get; set; }
        public BorderItem bottom { get; set; }
        public BorderItem left { get; set; }
    }
    public struct BorderItem
    {
        public Color color { get; set; }
        private int _width;
        public int width
        {
            get
            {
                if (style == BorderStyle.None)
                    return 0;
                return _width;
            }
            set
            { 
                    _width = value;
            }
        }
        public BorderStyle style { get; set; }
    }

    public enum BorderStyle
    {
        None = 0,
        Solid,
        Dotted,
        Dashed
    }
}
