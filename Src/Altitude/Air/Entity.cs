using System;

namespace Altitude.Air
{
    public abstract class Entity
    {
        private readonly Air _air;

        public Entity(Air air)
        {
            _air = air;
        }

        public Air Air
        {
            get { return _air; }
        }

        public object GetProperty(string name)
        {
            throw new NotImplementedException();
        }
    }
}

