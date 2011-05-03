using System;

namespace Altitude.Air
{
    public class PropertyDef
    {
        private readonly Air _air;

        public PropertyDef(Air air, string name)
        {
            _air = air;
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
    }
}

