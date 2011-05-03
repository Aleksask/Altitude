using System;

namespace Altitude.Air
{
    public class NodeDef
    {
        private readonly Air _air;

        public NodeDef(Air air, string name)
        {
            _air = air;
            Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        public Air Air
        {
            get { return _air; }
        }
    }
}

