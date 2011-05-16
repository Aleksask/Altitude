using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.Main.Model
{
    public interface IMessageFactory
    {
        IMessage CreateMessage(long id, string value, long[] categoryIds);
    }
}
