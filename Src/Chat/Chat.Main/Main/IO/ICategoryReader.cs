using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chat.Main
{
    public interface ICategoryReader : IDisposable
    {
        long Id { get; }

        string Name { get; }

        IList<long> IsAIds { get; }

        bool Read();
    }
}
