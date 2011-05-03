//
// Copyright (C) 2011 Thomas Mitchell
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Altitude.Air
{
    public class Node : Entity
    {
        private readonly NodeDef _nodeDef;
        private readonly Dictionary<string, object> _properties;
        private readonly List<Edge> _outEdges;
        private readonly List<Edge> _inEdges;

        public Node(Air air, NodeDef def)
            : base(air)
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
            return new Property(Air, propertyDefinition, _properties[propertyDefinition.Name]);
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

