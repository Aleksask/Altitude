using System;
using System.Linq;
using System.Collections.Generic;

namespace Altitude
{
    public static class EdgeEx
    {
        public static IEnumerable<Node> GetDestNodes(this IEnumerable<Edge> edges)
        {
            return edges.Select(f => f.Out);
        }

        public static IEnumerable<Node> GetSourceNodes(this IEnumerable<Edge> edges)
        {
            return edges.Select(f => f.In);
        }
    }
}

