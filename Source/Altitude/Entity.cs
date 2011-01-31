using System;
namespace Altitude
{
	public abstract class Entity
	{
        private readonly Altitude _altitude;

		public Entity (Altitude altitude)
		{
            _altitude = altitude;
		}

        public Altitude Altitude
        {
            get { return _altitude; }
        }

		public object GetProperty(string name)
		{
			throw new NotImplementedException();
		}
	}
}

