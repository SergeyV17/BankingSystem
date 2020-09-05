namespace BankingSystem.Models.Implementations.Data
{
    /// <summary>
    /// класс репозитория
    /// </summary>
    class Repository
    {
        private static Repository instance;

        private static readonly object locker;

        private readonly Node root;

        /// <summary>
        /// Конструктор репозитория
        /// </summary>
        /// <param name="root">корневой узел дерева</param>
        private Repository(Node root)
        {
            this.root = root;
        }

        /// <summary>
        /// Метод возвращающий репозиторий
        /// </summary>
        /// <param name="root">корневой узел дерева</param>
        /// <returns>репозиторий</returns>
        public static Repository GetRepository(Node root)
        {
            if (instance == null)
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new Repository(root);
                    }
                }
            }

            return instance;
        }
    }
}
