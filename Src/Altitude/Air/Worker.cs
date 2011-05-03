using System;

namespace Altitude.Air
{
    public class Worker
    {
        private readonly Air _air;

        public Worker(Air air)
        {
            _air = air;
        }

        public Air Air
        {
            get { return _air; }
        }
    }
}

