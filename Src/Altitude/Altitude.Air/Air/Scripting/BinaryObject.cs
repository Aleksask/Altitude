using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altitude.Air.Scripting
{
    public class BinaryObject<T> : BinaryObject where T : struct
    {
        public BinaryObject(T value)
            : base(value)
        { }

        public new T Value { get; private set; }
    }

    public class BinaryObject
    {
        public BinaryObject(object value)
        {
        }

        public byte Type { get; private set; }

        public bool HasValue { get; private set; }

        public object Value { get; private set; }
    }
}
