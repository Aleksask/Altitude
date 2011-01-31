using System;

namespace Altitude
{
	public class Worker
	{
        private readonly Altitude _altitude;

        public Worker(Altitude altitude)
		{
            _altitude = altitude;
		}

        public Altitude Altitude
        {
            get { return _altitude; }
        }
	}
}

