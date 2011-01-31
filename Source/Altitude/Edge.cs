using System;

namespace Altitude
{
	public class Edge : Entity
	{
		private readonly EdgeDef _edgeDef;

        public Edge(Altitude altitude, EdgeDef def, Node inNode, Node outNode)
            :base(altitude)
		{
			_edgeDef = def;
			In = inNode;
			Out = outNode;
			
			In.AddOutEdge(this);
			Out.AddInEdge(this);
		}

		public string EdgeDefName
		{
			get { return _edgeDef.Name; }
		}
		
		public Node In
		{
			get; private set;
		}
		
		public Node Out
		{
			get; private set;
		}
		
		public EdgeDef GetDef()
		{
			return _edgeDef;
		}
		
	}
}

