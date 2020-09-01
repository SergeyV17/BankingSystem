using System.Collections.Generic;

namespace BankingSystem.Models.Implementations.Data
{
    class Node
    {
        public string Name { get; }
        public Node Parent { get; private set; }

        public IList<Node> Children { get; }

        public Node(string name)
        {
            Name = name;
            Children = new List<Node>();
        }

        public void AddNode(Node node)
        {
            node.Parent = this;
            this.Children.Add(node);
        }
    }
}
