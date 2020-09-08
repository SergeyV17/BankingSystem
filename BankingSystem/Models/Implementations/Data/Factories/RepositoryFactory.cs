using System;
using System.Linq;
using System.Threading.Tasks;
using BankingSystem.Models.Implementations.BankServices.DepositService;
using BankingSystem.Models.Implementations.BankServices.DepositService.Factories;
using BankingSystem.Models.Implementations.Clients;
using BankingSystem.Models.Implementations.Clients.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.CompanyData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData.Factories;
using static BankingSystem.Models.Implementations.Data.Factories.AppFactories;

namespace BankingSystem.Models.Implementations.Data.Factories
{
    /// <summary>
    /// Класс фабрики репозитория
    /// </summary>
    class RepositoryFactory
    {
        private static readonly Random random;
        private static object locker;

        /// <summary>
        /// Конструктор репозитория
        /// </summary>
        static RepositoryFactory()
        {
            random = new Random();
            locker = new object();
        }

        /// <summary>
        /// Метод создания репозитория
        /// </summary>
        /// <param name="dbContext">контекст данных</param>
        /// <param name="quantity">кол-во клиентов</param>
        /// <returns>репозиторий</returns>
        public static Repository CreateRepository(int quantity)
        {
            var root = CreateRepositoryTree();

            using (AppDbContext context = new AppDbContext())
            {
                if (!context.Clients.Any())
                    FillRepository(context, quantity);
            }

            return Repository.GetRepository(root);
        }

        /// <summary>
        /// Метод создания дерева для репозитория
        /// </summary>
        /// <returns>корневой узел</returns>
        private static Node CreateRepositoryTree()
        {
            var root = new Node("Корень", NodeType.Intermediate);
            var bankingSystem = new Node("Банковская система", NodeType.Intermediate);

            root.AddNode(bankingSystem);

            var individual = new Node("Физические лица", NodeType.Individual);
            var entity = new Node("Юридические лица", NodeType.Entity);
            var VIPClients = new Node("Вип клиенты", NodeType.Intermediate);

            bankingSystem.AddNode(individual);
            bankingSystem.AddNode(entity);
            bankingSystem.AddNode(VIPClients);

            var VIPIndividual = new Node("Физические лица", NodeType.VIPIndividual);
            var VIPEntity = new Node("Юридические лица", NodeType.VIPEntity);

            VIPClients.AddNode(VIPIndividual);
            VIPClients.AddNode(VIPEntity);

            return root;
        }

        /// <summary>
        /// Метод заполнения репозитория клиентами
        /// </summary>
        /// <param name="dbContext">контекст данных</param>
        /// <param name="quantity">кол-во клиентов</param>
        private static void FillRepository(AppDbContext dbContext, int quantity)
        {
            Parallel.For(1, quantity + 1, (i) =>
            {
                lock (locker)
                {
                    Client client = null;

                    decimal balance = random.Next(1, 10) * 10000;
                    bool capitalization = Convert.ToBoolean(random.Next(2));

                    var passport = PassportFactory.CreatePassport(
                        FullNameFactory.CreateFullName(Gender.Female),
                        SeriesAndNumberFactory.CreateSeriesAndNumber(),
                        $"Адрес_{i}");

                    switch (random.Next(Enum.GetNames(typeof(ClientType)).Length))
                    {
                        case (int)ClientType.Individual:

                            var contact = ContactFactory.CreateContact(PhoneNumberFactory.CreateNumber(), $"Client@Email.ru_{i}");

                            var individualAccount = AppFactories.AccountFactories[random.Next(AccountFactories.Length)].CreateAccount(
                                    IndividualsCardFactories[random.Next(IndividualsCardFactories.Length)].CreateCard(balance),
                                    random.Next(2) == 0 ? new DefaultDepositFactory().CreateDeposit(balance, capitalization, ClientType.Individual) : new NullDeposit());

                            client = IndividualFactory.CreateIndividual(passport, contact, individualAccount);
                            break;

                        case (int)ClientType.Entity:

                            contact = ContactFactory.CreateContact(PhoneNumberFactory.CreateNumber(), $"Client@Email.ru_{i}");

                            var entityAccount = AccountFactories[random.Next(AccountFactories.Length)].CreateAccount(
                                    EntitiesCardFactories[random.Next(EntitiesCardFactories.Length)].CreateCard(balance),
                                    random.Next(2) == 0 ? new DefaultDepositFactory().CreateDeposit(balance, capitalization, ClientType.Entity) : new NullDeposit());

                            var company = new Company($"Компания_{i}", $"Company.Website.ru_{i}");

                            client = EntityFactory.CreateEntity(passport, contact, entityAccount, company);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    dbContext.Add(client);
                }
            });

            dbContext.SaveChanges();
        }
    }
}
