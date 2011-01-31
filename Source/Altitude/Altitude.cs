using System;
using System.Collections.Generic;
using System.Linq;

namespace Altitude
{
	public class Altitude : IDisposable
	{
        private AltitudeConfiguration _configuration;
        private List<IService> _services;
		private Dictionary<string, NodeDef> _nodeDefsByName;
		private Dictionary<string, EdgeDef> _edgeDefsByName;
		private Dictionary<string, PropertyDef> _propertyDefsByName;
		private List<Node> _nodes;

        public Altitude(AltitudeConfiguration config)
		{
            _configuration = config;
            _services = new List<IService>();
			_nodeDefsByName = new Dictionary<string, NodeDef>();
			_edgeDefsByName = new Dictionary<string, EdgeDef>();
			_propertyDefsByName = new Dictionary<string, PropertyDef>();
			_nodes = new List<Node>();
			
			Workers = CreateWorkers();
		}

        public AltitudeConfiguration Configuration
        {
            get { return _configuration; }
        }

        public List<IService> Services
        {
            get { return _services; }
        }

		public List<Worker> Workers
		{
			get; private set;
		}
	
		public IEnumerable<NodeDef> GetNodeDefs()
		{
			return _nodeDefsByName.Values;
		}
		
		private List<Worker> CreateWorkers()
		{
			List<Worker> workers = new List<Worker>();
            workers.Add(new Worker(this));			
			return workers;
		}

        public void Connect()
        {
        }

		public Transaction CreateTransaction()
		{
			return new Transaction(this);
		}
		
		public NodeDef GetNodeDef(string name)
		{
			return _nodeDefsByName[name];
		}
		
		public NodeDef CreateNodeDef(string name)
		{
			NodeDef nodeDef = new NodeDef(this, name);
			_nodeDefsByName.Add(nodeDef.Name, nodeDef);
			return nodeDef;
		}

        public Node CreateNode(NodeDef definition)
        {
            Node node = new Node(this, definition);
            _nodes.Add(node);
            return node;
        }

		public EdgeDef CreateEdgeDef(string name)
		{
			EdgeDef edgeDef = new EdgeDef(this, name);
			_edgeDefsByName.Add(edgeDef.Name, edgeDef);
			return edgeDef;
		}
		
		public PropertyDef CreatePropertyDef(string name)
		{
			PropertyDef propertyDef = new PropertyDef(this, name);
			_propertyDefsByName.Add(propertyDef.Name, propertyDef);
			return propertyDef;
		}
		
		public EdgeDef GetEdgeDef(string name)
		{
			return _edgeDefsByName[name];
		}
		
		public PropertyDef GetPropertyDef(string name)
		{
			return _propertyDefsByName[name];
		}
		
		public IList<Node> GetNodes()
		{
			return _nodes;	
		}
		
		public void Dispose ()
		{
		}
	}
}

