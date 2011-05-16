using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.Main.Model
{
    public interface IMessage
    {
        long Id { get; }

        string Value { get; }

        long[] CategoryIds { get; }
    }
}
