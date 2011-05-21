using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.Main.Model
{
    public class MessageInfo : IMessageInfo
    {
        private readonly long _authorId;
        private readonly string _text;
        private readonly long[] _categoryIds;
        private readonly DateTimeOffset _created;

        public MessageInfo(string text, IUser author, IEnumerable<ICategory> categories, DateTimeOffset created)
        {
            _authorId = author.Id;
            _categoryIds = categories.Select(f => f.Id).ToArray();
            _text = text;
            _created = created;
        }

        public DateTimeOffset Created
        {
            get { return _created; }
        }

        public long AuthorId
        {
            get { return _authorId; }
        }

        public string Text
        {
            get { return _text; }
        }

        public long[] CategoryIds
        {
            get { return _categoryIds; }
        }
    }
}