using System;

namespace Altitude
{
	public class EdgeDef
	{
        private readonly Altitude _altitude;

        public EdgeDef(Altitude altitude, string name)
		{
            _altitude = altitude;
			Name = name;
		}

        public Altitude Altitude
        {
            get { return _altitude; }
        }
		
		public string Name
		{
			get; private set;
		}
		
		public Edge CreateEdge(Node source, Node destination)
		{
            return new Edge(Altitude, this, source, destination);
		}
	}
}

