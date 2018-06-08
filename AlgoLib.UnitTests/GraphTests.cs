using System;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoLib.UnitTests
{
    public class ColorNode
    {
        public int NodeNumber { get; set; }
        public Color NodeColor { get; set; }
    }

    [TestClass]
    public class GraphTests
    {
        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private TestContext testContextInstance;

        /// <summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void Graph_Valid_Success()
        {
            Graph<string> web = new Graph<string>();
            var privacyNode = web.AddNode("Privacy.htm");
            var peopleNode = web.AddNode("People.aspx");
            var aboutNode = web.AddNode("About.htm");
            var indexNode = web.AddNode("Index.htm");
            var productsNode = web.AddNode("Products.aspx");
            var contactsNode = web.AddNode("Contact.aspx");
            Assert.IsTrue(web.Contains("Privacy.htm"));
            Assert.IsFalse(web.Contains("Home.htm"));
            Assert.IsTrue(web.Contains(peopleNode));
            Assert.IsFalse(web.Remove("Home.htm"));
            Assert.IsTrue(web.Remove("About.htm"));
            Assert.IsFalse(web.Contains("About.htm"));
            Assert.IsFalse(web.Contains(aboutNode));
        }
        [TestMethod]
        public void Graph_ValidEdges_Success()
        {
            Graph<string> web = new Graph<string>();
            var privacyNode = web.AddNode("Privacy.htm");
            var peopleNode = web.AddNode("People.aspx");
            var aboutNode = web.AddNode("About.htm");
            var indexNode = web.AddNode("Index.htm");
            var productsNode = web.AddNode("Products.aspx");
            var contactsNode = web.AddNode("Contact.aspx");
            // now make the edges
            web.AddUndirectedEdge(peopleNode, privacyNode);  // People -> Privacy
            //web.AddDirectedEdge(peopleNode, privacyNode);  // People -> Privacy
            //web.AddDirectedEdge("Privacy.htm", "Index.htm");    // Privacy -> Index
            //web.AddDirectedEdge("Privacy.htm", "About.htm");    // Privacy -> About

            //web.AddDirectedEdge("About.htm", "Privacy.htm");    // About -> Privacy
            //web.AddDirectedEdge("About.htm", "People.aspx");    // About -> People
            //web.AddDirectedEdge("About.htm", "Contact.aspx");   // About -> Contact

            //web.AddDirectedEdge("Index.htm", "About.htm");      // Index -> About
            //web.AddDirectedEdge("Index.htm", "Contact.aspx");   // Index -> Contacts
            //web.AddDirectedEdge("Index.htm", "Products.aspx");  // Index -> Products

            //web.AddDirectedEdge("Products.aspx", "Index.htm");  // Products -> Index
            //web.AddDirectedEdge("Products.aspx", "People.aspx");// Products -> People
        }
        [TestMethod]
        public void Graph_ConnectedEdges_Success()
        {
            Graph<ColorNode> board = new Graph<ColorNode>();
            var node0 = board.AddNode(new ColorNode() { NodeNumber = 0, NodeColor = Color.Green});
            var node1 = board.AddNode(new ColorNode() { NodeNumber = 1, NodeColor = Color.Red });
            var node2 = board.AddNode(new ColorNode() { NodeNumber = 2, NodeColor = Color.Green });
            var node3 = board.AddNode(new ColorNode() { NodeNumber = 3, NodeColor = Color.Green });
            var node4 = board.AddNode(new ColorNode() { NodeNumber = 4, NodeColor = Color.Green });
            var node5 = board.AddNode(new ColorNode() { NodeNumber = 5, NodeColor = Color.Red });
            board.AddDirectedEdge(node0, node2);
            board.AddDirectedEdge(node1, node3);
            board.AddDirectedEdge(node2, node0);
            board.AddDirectedEdge(node2, node3);
            board.AddDirectedEdge(node2, node4);
            board.AddDirectedEdge(node3, node1);
            board.AddDirectedEdge(node3, node2);
            board.AddDirectedEdge(node3, node5);
            board.AddDirectedEdge(node4, node2);
            board.AddDirectedEdge(node5, node3);
            for(int nodeIndex = 0;nodeIndex < board.Nodes.Count(); nodeIndex++)
            {
                var node = board.Nodes[nodeIndex];
                int neighbourIndex = 0;
                while (neighbourIndex < node.Neighbors.Count)
                {
                    var neighbour = node.Neighbors[neighbourIndex];
                    if (neighbour.Value.NodeNumber != node.Value.NodeNumber &&
                        neighbour.Value.NodeColor == node.Value.NodeColor)
                    {
                        var nodeNumber = neighbour.Value.NodeNumber;
                        node.Neighbors.RemoveAt(neighbourIndex);
                        var newNeighbours = board.Nodes
                            .Find(t => t.Value.NodeNumber == nodeNumber)
                            .Neighbors
                            .Where(n => n.Value.NodeNumber != node.Value.NodeNumber);
                        foreach (var newNeighbour in newNeighbours)
                        {
                            // append the current node to the end
                            board.AddDirectedEdge(newNeighbour.Value,node.Value);
                        }
                        node.Neighbors.AddRange(newNeighbours);
                        //node.Neighbors.Remove(neighbour.Value.NodeNumber);
                        // remove this element from the graph
                        board.Remove(neighbour.Value);
                    }
                    else
                    {
                        neighbourIndex++; // point to the next edge
                    }
                }
            }
            foreach (var node in board.Nodes)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var neighbour in node.Neighbors)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.AppendFormat("{0}", neighbour.Value.NodeNumber);
                }
                TestContext.WriteLine(string.Format("{0} {1}: {2}",node.Value.NodeNumber,
                    HexConverter(node.Value.NodeColor),sb.ToString()));
            }
        }
    }
}
