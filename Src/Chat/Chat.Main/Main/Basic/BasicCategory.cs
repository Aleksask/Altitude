using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chat.Main.Model;

namespace Chat.Main.Basic
{
    class BasicCategory : ICategory
    {
        private long _id;
        private string _name;

        public BasicCategory(long id, string name)
        {
            _id = id;
            _name = name;
        }

        public long Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
