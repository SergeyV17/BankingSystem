using System.Collections.Generic;

namespace BankingSystem.Models.Implementations.Data
{
    /// <summary>
    /// Класс узла дерева
    /// </summary>
    partial class Node
    {
        /// <summary>
        /// Конструктор узла дерева
        /// </summary>
        /// <param name="name">наименование</param>
        public Node(string name, NodeType type)
        {
            Name = name;
            Type = type;
            Children = new List<Node>();
        }

        public string Name { get; }
        public NodeType Type { get; }
        public Node Parent { get; private set; }
        public IList<Node> Children { get; }

        /// <summary>
        /// Метод добавления узла
        /// </summary>
        /// <param name="node">добавляемый узел</param>
        public void AddNode(Node node)
        {
            node.Parent = this;
            this.Children.Add(node);
        }
    }
}
