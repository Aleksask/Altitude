using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altitude.Air.Scripting
{
    public class Graph
    {
        public Graph(string name)
        {

        }

        public void LoadGraph()
        {

        }

        public void GetExplicitLock(Guid current_cell_guid, Guid owner_guid, int timeout)
        {
        }

        public void ExplicitUnlock(Guid current_cell_guid, Guid owner_guid)
        {
        }
        
        public Cell LoadCell(Guid current_cell_guid)
        {
            throw new NotImplementedException();
        }

        public void SaveCell(Cell cell)
        {
        }

        public IEnumerable<Guid> CellIDSet { get; private set; }
    }
}
