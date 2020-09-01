namespace BankingSystem.Models.Implementations.Data
{
    class Repository
    {
        private static Repository instance;

        private static readonly object locker;

        private readonly Node root;

        private Repository(Node root)
        {
            this.root = root;
        }

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
