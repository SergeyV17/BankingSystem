using System;

namespace BankingSystem.Models.Implementations.Data
{
    /// <summary>
    /// Класс репозитория
    /// </summary>
    class Repository
    {
        private static readonly Lazy<Repository> lazy =
            new Lazy<Repository>(() => new Repository());

        public Node Node { get; private set; }

        /// <summary>
        /// Конструктор репозитория
        /// </summary>
        private Repository() { }

        /// <summary>
        /// Метод возвращающий экземпляр репозитория
        /// </summary>
        /// <param name="root">корневой узел дерева</param>
        /// <returns>репозиторий</returns>
        public static Repository GetRepository(Node root)
        {
            lazy.Value.Node = root;
            return lazy.Value;
        }

        /// <summary>
        /// Метод возвращающий экземпляр репозитория
        /// </summary>
        /// <returns>репозиторий</returns>
        public static Repository GetRepository() => lazy.Value;
    }
}
