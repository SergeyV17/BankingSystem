using System;
using System.Linq;
using System.Threading.Tasks;
using BankingSystem.Models.Implementations.Accounts.Factories;
using BankingSystem.Models.Implementations.BankServices.CardService.Factories;
using BankingSystem.Models.Implementations.BankServices.DepositService.Factories;
using BankingSystem.Models.Implementations.Clients;
using BankingSystem.Models.Implementations.Clients.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.CompanyData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData.Factories;

namespace BankingSystem.Models.Implementations.Data.Factories
{
    /// <summary>
    /// Класс фабрики репозитория
    /// </summary>
    class RepositoryFactory
    {
        private static readonly Random random;
        private static object locker;

        // массивы с фабриками для возможности выбора рандомной фабрики
        private static readonly AccountFactory[] accountFactories;
        private static readonly CardFactory[] individualsCardFactories;
        private static readonly CardFactory[] entitiesCardFactories;

        /// <summary>
        /// Конструктор репозитория
        /// </summary>
        static RepositoryFactory()
        {
            random = new Random();
            locker = new object();

            accountFactories = new AccountFactory[]
            {
                new RegularAccountFactory(), new VipAccountFactory()
            };

            individualsCardFactories = new CardFactory[]
            {
                new VisaClassicFactory(), new VisaBlackFactory(), new VisaPlatinumFactory()
            };

            entitiesCardFactories = new CardFactory[]{ new VisaCorporateFactory()};
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
            var root = new Node("Корень");
            var bankingSystem = new Node("Банковская система");

            root.AddNode(bankingSystem);

            var individual = new Node("Физические лица");
            var entity = new Node("Юридические лица");
            var VIPClients = new Node("Вип клиенты");

            bankingSystem.AddNode(individual);
            bankingSystem.AddNode(entity);
            bankingSystem.AddNode(VIPClients);

            var VIPIndividual = new Node("Физические лица");
            var VIPEntity = new Node("Юридические лица");

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
            Parallel.For(0, quantity, (i) =>
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

                            var individualAccount = accountFactories[random.Next(accountFactories.Length)].CreateAccount(
                                    individualsCardFactories[random.Next(individualsCardFactories.Length)].CreateCard(balance),
                                    new DefaultDepositFactory().CreateDeposit(balance, capitalization, ClientType.Individual));

                            client = IndividualFactory.CreateIndividual(passport, contact, individualAccount);
                            break;

                        case (int)ClientType.Entity:

                            contact = ContactFactory.CreateContact(PhoneNumberFactory.CreateNumber(), $"Client@Email.ru_{i}");

                            var entityAccount = accountFactories[random.Next(accountFactories.Length)].CreateAccount(
                                    entitiesCardFactories[random.Next(entitiesCardFactories.Length)].CreateCard(balance),
                                    new DefaultDepositFactory().CreateDeposit(balance, capitalization, ClientType.Entity));

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
