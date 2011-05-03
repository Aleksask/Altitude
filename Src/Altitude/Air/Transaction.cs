using System;

namespace Altitude.Air
{
    public class Transaction : IDisposable
    {
        private readonly Air _air;

        public Transaction(Air air)
        {
            _air = air;
        }

        public Air Air
        {
            get { return _air; }
        }

        public void Dispose()
        {
        }
    }
}

