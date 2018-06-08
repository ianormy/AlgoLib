using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoLib
{
    public class Graph<T>
    {
        private List<GraphNode<T>> _nodeSet;

        public Graph() : this(null) { }
        public Graph(List<GraphNode<T>> nodeSet)
        {
            if (nodeSet == null)
                this._nodeSet = new List<GraphNode<T>>();
            else
                this._nodeSet = nodeSet;
        }

        public void AddNode(GraphNode<T> node)
        {
            // adds a node to the graph
            _nodeSet.Add(node);
        }

        public GraphNode<T> AddNode(T value)
        {
            // adds a node to the graph
            var node = new GraphNode<T>(value);
            _nodeSet.Add(node);
            return node;
        }

        public void AddDirectedEdge(T from, T to)
        {
            GraphNode<T> nodefrom = (GraphNode<T>)_nodeSet.Find(n => n.Value.Equals(from));
            GraphNode<T> nodeTo = (GraphNode<T>)_nodeSet.Find(n => n.Value.Equals(to));
            AddDirectedEdge(nodefrom,nodeTo);
        }

        public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);
        }

        public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to)
        {
            from.Neighbors.Add(to);
        }

        public void AddUndirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);

            to.Neighbors.Add(from);
            to.Costs.Add(cost);
        }

        public void AddUndirectedEdge(GraphNode<T> from, GraphNode<T> to)
        {
            from.Neighbors.Add(to);
            to.Neighbors.Add(from);
        }

        public bool Contains(GraphNode<T> value)
        {
            return _nodeSet.Contains(value);
        }

        public bool Contains(T value)
        {
            return _nodeSet.Find(n => n.Value.Equals(value)) != null;
        }

        public bool Remove(T value)
        {
            // first remove the node from the nodeset
            GraphNode<T> nodeToRemove = (GraphNode<T>)_nodeSet.Find(n => n.Value.Equals(value));
            if (nodeToRemove == null)
                // node wasn't found
                return false;

            // otherwise, the node was found
            _nodeSet.Remove(nodeToRemove);

            // enumerate through each node in the nodeSet, removing edges to this node
            foreach (GraphNode<T> gnode in _nodeSet)
            {
                int index = gnode.Neighbors.IndexOf(nodeToRemove);
                if (index != -1)
                {
                    // remove the reference to the node and associated cost
                    gnode.Neighbors.RemoveAt(index);
                    // check to see if we have been storing costs as well...
                    if (gnode.Costs.Count > 0)
                    {
                        gnode.Costs.RemoveAt(index);
                    }
                }
            }

            return true;
        }

        public List<GraphNode<T>> Nodes => _nodeSet;

        public int Count => _nodeSet.Count;
    }
}
