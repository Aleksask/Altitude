using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chat.Main.Model;

namespace Chat.Main.Basic
{
    class BasicMessage : IMessage
    {
        private long _id;
        private string _value;
        private long[] _categoryIds;

        public BasicMessage(long id, string value, long[] categoryIds)
        {
            _id = id;
            _value = value;
            _categoryIds = categoryIds;
        }

        public long Id
        {
            get { return _id; }
        }

        public string Value
        {
            get { return _value; }
        }

        public long[] CategoryIds
        {
            get { return _categoryIds; }
        }
    }
}
