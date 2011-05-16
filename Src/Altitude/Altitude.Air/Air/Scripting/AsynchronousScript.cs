using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altitude.Air.Scripting
{
    public abstract class AsynchronousScript
    {
        protected bool initialised = false;
        protected Graph graph;
        protected string graph_name;

        public abstract Graph Initialise();

        public abstract bool Action(Guid current_cell_guid);

        public abstract object GetResult();

        public void PassActionScript(object tp)
        {
        }
    }
}
