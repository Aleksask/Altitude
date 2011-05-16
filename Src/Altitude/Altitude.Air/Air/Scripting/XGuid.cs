using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altitude.Air.Scripting
{
    public class XGuid : BinaryObject<Guid>
    {
        public XGuid(Guid value)
            : base(value)
        { }
    }
}
