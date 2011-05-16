using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.Main.Model
{
    public interface ICategoryFactory
    {
        ICategory CreateCategory(long id, string name);
    }
}
