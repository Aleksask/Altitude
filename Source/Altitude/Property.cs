using System;
namespace Altitude
{
	public class Property
	{
        private readonly Altitude _altitude;
        private readonly PropertyDef _propertyDef;

        public Property(Altitude altitude, PropertyDef def, object value)
		{
            _altitude = altitude;
			_propertyDef = def;
			Value = value;
		}

        public Altitude Altitude
        {
            get { return _altitude; }
        }

		public string PropertyDefName
		{
			get { return _propertyDef.Name; }
		}
		
		public object Value
		{
			get; set;
		}
		
		public PropertyDef GetPropertyDef()
		{
			return _propertyDef;
		}
	}
}

