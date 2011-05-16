using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chat.Main.Model;

namespace Chat.Main.Basic
{
    class BasicMessageFactory : IMessageFactory
    {
        public IMessage CreateMessage(long id, string value, long[] categoryIds)
        {
            return new BasicMessage(id, value, categoryIds);
        }
    }
}
