using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoLib
{
    public class GraphNode<T> : Node<T>
    {
        private List<int> costs;
        private List<Node<T>> _nodes;

        public GraphNode() : base() { }
        public GraphNode(T value) : base(value) { }
        public GraphNode(T value, List<Node<T>> neighbors) : base(value, neighbors) { }

        new public List<Node<T>> Neighbors
        {
            get
            {
                if (base.Neighbors == null)
                    base.Neighbors = new List<Node<T>>();

                return base.Neighbors;
            }
        }

        public List<int> Costs
        {
            get
            {
                if (costs == null)
                    costs = new List<int>();

                return costs;
            }
        }
    }
}
