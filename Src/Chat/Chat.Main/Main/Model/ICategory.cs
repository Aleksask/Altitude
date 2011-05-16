using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.Main.Model
{
    public interface ICategory
    {
        long Id { get; }

        string Name { get; }
    }
}
