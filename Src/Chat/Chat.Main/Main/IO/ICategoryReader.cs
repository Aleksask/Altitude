using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.Main
{
    public interface ICategoryReader : IDisposable
    {
        int Id { get; }

        string Name { get; }

        IList<int> IsAIds { get; }

        bool Read();
    }
}
