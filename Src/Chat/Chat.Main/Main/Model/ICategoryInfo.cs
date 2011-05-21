using System.Collections.Generic;

namespace Chat.Main.Model
{
    public interface ICategoryInfo
    {
        string Name { get; }

        IList<ICategoryInfo> Children { get; }

        IList<ICategoryInfo> Parents { get; }
    }
}