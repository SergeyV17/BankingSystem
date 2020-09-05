using System.Collections.Generic;

namespace BankingSystem.Models.Implementations.Data
{
    /// <summary>
    /// Класс узла дерева
    /// </summary>
    class Node
    {
        /// <summary>
        /// Конструктор узла дерева
        /// </summary>
        /// <param name="name">наименование</param>
        public Node(string name)
        {
            Name = name;
            Children = new List<Node>();
        }

        public string Name { get; }
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
