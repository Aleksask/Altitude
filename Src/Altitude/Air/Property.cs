using System;

namespace Altitude.Air
{
    public class Property
    {
        private readonly Air _altitude;
        private readonly PropertyDef _propertyDef;

        public Property(Air altitude, PropertyDef def, object value)
        {
            _altitude = altitude;
            _propertyDef = def;
            Value = value;
        }

        public Air Air
        {
            get { return _altitude; }
        }

        public string PropertyDefName
        {
            get { return _propertyDef.Name; }
        }

        public object Value
        {
            get;
            set;
        }

        public PropertyDef GetPropertyDef()
        {
            return _propertyDef;
        }
    }
}

