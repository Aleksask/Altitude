using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altitude.Air.Scripting
{
    public class Cell
    {
        public string Name { get; private set; }

        public Dictionary<BinaryObject, BinaryObject> KeyValueTable { get; private set; }

        public IList<Guid> DestinationCellSet { get; private set; }

        public IList<Guid> UndirectedCellSet { get; private set; }

        public object GetValueByKey(BinaryObject key)
        {
            throw new NotImplementedException();
        }

        public void SetKeyValue(BinaryObject key, BinaryObject value)
        {
        }
    }
}
