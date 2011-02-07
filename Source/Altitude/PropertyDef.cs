using System;
namespace Altitude
{
    public class PropertyDef
    {
        private readonly Altitude _altitude;

        public PropertyDef(Altitude app, string name)
        {
            Name = name;
        }

        public Altitude Altitude
        {
            get { return _altitude; }
        }

        public string Name
        {
            get;
            private set;
        }
    }
}

