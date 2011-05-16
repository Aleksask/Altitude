using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altitude.Air.Scripting
{
    public class SyncShortestPath : SynchronousScript
    {
        private static BinaryObject bkey_distance;
        private static BinaryObject bkey_parent;

        private Guid source_cell_guid;

        public override Graph Initialise()
        {
            if (!initialised)
            {
                graph = new Graph(graph_name);
                graph.LoadGraph();
                Parallel.ForEach<Guid>(graph.CellIDSet, cell_guid =>
                {
                    Cell cell = graph.LoadCell(cell_guid);
                    cell.SetKeyValue(bkey_distance, new XInt32(Int32.MaxValue));
                    cell.SetKeyValue(bkey_parent, new XGuid(Guid.Empty));
                    graph.SaveCell(cell);
                }
                );
                initialised = true;
            }
            return graph;
        }

        public override bool Action(Guid current_cell_guid)
        {
            Initialise();
            bool updated = false;
            Guid owner_guid = Guid.NewGuid();

            graph.GetExplicitLock(current_cell_guid, owner_guid, 200);
            Cell cell = graph.LoadCell(current_cell_guid);
            XInt32 current_distance = (XInt32)cell.GetValueByKey(bkey_distance);
            Int32 shortest_distance = current_distance.Value;
            Guid parent_guid = Guid.Empty;

            if (current_cell_guid == source_cell_guid)
                shortest_distance = 0;

            foreach (BinaryObject bo in cell.KeyValueTable.Keys)
            {
                if (bo.Type == (byte)BuiltInType.Guid)
                {
                    XInt32 parent_distance = (XInt32)cell.KeyValueTable[bo];
                    if (parent_distance.Value != Int32.MaxValue)
                    {
                        if (parent_distance.Value + 1 < shortest_distance)
                        {
                            shortest_distance = parent_distance.Value + 1;
                            parent_guid = ((XGuid)bo).Value;
                        }
                    }
                }
            }

            if (shortest_distance < current_distance.Value)
            {
                cell.SetKeyValue(bkey_distance, new XInt32(shortest_distance));
                cell.SetKeyValue(bkey_parent, new XGuid(parent_guid));
                updated = true;
            }

            graph.SaveCell(cell);
            graph.ExplicitUnlock(current_cell_guid, owner_guid);

            if (updated)
            {
                Parallel.ForEach<Guid>(cell.DestinationCellSet, cell_guid =>
                {
                    graph.GetExplicitLock(cell_guid, owner_guid, 200);
                    Cell dest_cell = graph.LoadCell(cell_guid);
                    dest_cell.SetKeyValue(new XGuid(current_cell_guid), new XInt32(shortest_distance));
                    graph.SaveCell(dest_cell);
                    graph.ExplicitUnlock(cell_guid, owner_guid);
                });
                Parallel.ForEach<Guid>(cell.UndirectedCellSet, cell_guid =>
                {
                    graph.GetExplicitLock(cell_guid, owner_guid, 200);
                    Cell dest_cell = graph.LoadCell(cell_guid);
                    dest_cell.SetKeyValue(new XGuid(current_cell_guid), new XInt32(shortest_distance));
                    graph.SaveCell(dest_cell);
                    graph.ExplicitUnlock(cell_guid, owner_guid);
                });
            }
            return updated;
        }

        public override object GetResult()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Guid cell_guid in graph.CellIDSet)
            {
                Cell cell = graph.LoadCell(cell_guid);
                XInt32 shortest_distance = (XInt32)cell.GetValueByKey(bkey_distance);
                sb.AppendLine(cell.Name + "\t" + shortest_distance);
                sb.AppendLine("The shortest part of " + cell.Name + " is: " + shortest_distance);
                Stack<string> path = new Stack<string>();
                GetShortestPath(cell_guid, ref path);
                foreach (string cell_name in path)
                {
                    sb.Append(cell_name + "\t");
                }
                sb.AppendLine();
            }
            return sb;
        }

        private void GetShortestPath(Guid cell_guid, ref Stack<string> path)
        {
            Cell cell = graph.LoadCell(cell_guid);
            path.Push(cell.Name);
            XGuid parent = (XGuid)cell.GetValueByKey(bkey_parent);
            if (parent.Value != Guid.Empty)
                GetShortestPath(parent.Value, ref path);
        }
    }
}
