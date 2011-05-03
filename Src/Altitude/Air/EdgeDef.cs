using System;

namespace Altitude.Air
{
    public class EdgeDef
    {
        private readonly Air _air;

        public EdgeDef(Air air, string name)
        {
            _air = air;
            Name = name;
        }

        public Air Air
        {
            get { return _air; }
        }

        public string Name
        {
            get;
            private set;
        }

        public Edge CreateEdge(Node source, Node destination)
        {
            return new Edge(Air, this, source, destination);
        }
    }
}

