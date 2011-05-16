using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chat.Main.Model;

namespace Chat.Main.Basic
{
    internal class BasicCategoryFactory : ICategoryFactory
    {
        public ICategory CreateCategory(long id, string name)
        {
            return new BasicCategory(id, name);
        }
    }
}
