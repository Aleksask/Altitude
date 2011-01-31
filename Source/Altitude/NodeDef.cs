using System;
namespace Altitude
{
	public class NodeDef
	{
        private readonly Altitude _altitude;

        public NodeDef(Altitude altitude, string name)
		{
            _altitude = altitude;
			Name = name;
		}
		
		public string Name
		{
			get; private set;
		}

        public Altitude Altitude
        {
            get { return _altitude; }
        }
	}
}

