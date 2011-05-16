using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altitude.Air.Scripting
{
    public class XInt32 : BinaryObject<Int32>
    {
        public XInt32(Int32 value)
            : base(value)
        { }
    }
}
