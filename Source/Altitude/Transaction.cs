using System;

namespace Altitude
{
	public class Transaction : IDisposable
	{
        private readonly Altitude _altitude;

        public Transaction(Altitude altitude)
		{
            _altitude = altitude;
		}

        public Altitude Altitude
        {
            get { return _altitude; }
        }
		
		public void Dispose ()
		{
		}
	}
}

