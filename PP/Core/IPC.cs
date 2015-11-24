using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PP.Core
{
    public interface IPC
    {
        Size BSize { get; }
        Padding padding { get; set; }
        Margin margin { get; set; }

    }
}
