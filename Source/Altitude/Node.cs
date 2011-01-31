using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Altitude
{
	public class Node : Entity
	{
        private readonly NodeDef _nodeDef;
		private readonly Dictionary<string, object> _properties;
		private readonly List<Edge> _outEdges;
		private readonly List<Edge> _inEdges;

        public Node(Altitude altitude, NodeDef def)
            : base(altitude)
		{
			_nodeDef = def;
			_properties = new Dictionary<string, object>();
			_outEdges = new List<Edge>();
			_inEdges = new List<Edge>();
		}
	
		public string NodeDefName
		{
			get { return _nodeDef.Name; }
		}
		
		public NodeDef GetNodeDef()
		{
			return _nodeDef;
		}
		
		public IEnumerable<Edge> GetEdges()
		{
			return _inEdges.Concat(_outEdges);
		}
		
		public IEnumerable<Edge> GetInEdges()
		{
			return _inEdges;
		}
		
		public IEnumerable<Edge> GetOutEdges()
		{
			return _outEdges;
		}
		
		public Edge CreateEdgeTo(Node node, EdgeDef edgeDefinition)
		{
			return edgeDefinition.CreateEdge(this, node);
		}
		
		public void SetProperty(PropertyDef propertyDefinition, object value)
		{
			if (_properties.ContainsKey(propertyDefinition.Name))
				_properties[propertyDefinition.Name] = value;
			else
				_properties.Add(propertyDefinition.Name, value);
		}
		
		public Property GetProperty(PropertyDef propertyDefinition)
		{
			return new Property(Altitude, propertyDefinition, _properties[propertyDefinition.Name]);
		}
		
		internal void AddInEdge(Edge edge)
		{
			_inEdges.Add(edge);
		}
		
		internal void AddOutEdge(Edge edge)
		{
			_outEdges.Add(edge);	
		}
			
		public void Delete()
		{
		}
	}
}

