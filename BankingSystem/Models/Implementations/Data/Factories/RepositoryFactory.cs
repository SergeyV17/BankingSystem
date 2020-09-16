using DataLibrary.Clients;
using DataLibrary.Clients.Factories;
using DataLibrary.Deposits;
using DataLibrary.Deposits.Factories;
using DbInteraction;
using RequisitesLibrary;
using RequisitesLibrary.ClientRequisites.CompanyData.Factories;
using RequisitesLibrary.ClientRequisites.ContactData.Factories;
using RequisitesLibrary.ClientRequisites.Factories;
using RequisitesLibrary.ClientRequisites.PassportData;
using RequisitesLibrary.ClientRequisites.PassportData.Factories;
using RequisitesLibrary.PassportData.Factories;
using System;
using System.Linq;
using System.Threading.Tasks;
using static BankingSystem.Models.Implementations.Data.Factories.AppFactories;

namespace BankingSystem.Models.Implementations.Data.Factories
{
    /// <summary>
    /// Класс фабрики репозитория
    /// </summary>
    class RepositoryFactory
    {
        public static event Action<int> ProcessingCountEvent;
        public static int Count { get; private set; }

        private static readonly Random random;
        private static readonly object locker;

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

            ProcessingCountEvent?.Invoke(Count++);
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

                    decimal cardBalance = random.Next(1, 10) * 10000;
                    decimal depositBalance = random.Next(1, 10) * 10000;
                    bool capitalization = Convert.ToBoolean(random.Next(2));

                    //Паспортные данные
                    var passport = PassportFactory.CreatePassport(
                        FullNameFactory.CreateFullName(Gender.Female),
                        SeriesAndNumberFactory.CreateSeriesAndNumber(),
                        $"Адрес_{i}");

                    //Контактные данные
                    var contact = ContactFactory.CreateContact(PhoneNumberFactory.CreateNumber(), $"Client@Email_{i}.ru");

                    switch (random.Next(Enum.GetNames(typeof(ClientType)).Length))
                    {
                        case (int)ClientType.Individual:

                            //Данные аккаунта
                            var card = IndividualsCardFactories[random.Next(IndividualsCardFactories.Length)].CreateCard(cardBalance);
                            var deposit = random.Next(2) == 0 ? 
                                                new DefaultDepositFactory().CreateDeposit(depositBalance, capitalization, ClientType.Individual) : 
                                                new NullDeposit();

                            var individualAccount = AccountFactories[random.Next(AccountFactories.Length)].CreateAccount(card, deposit);

                            //Создание физ. лица
                            client = IndividualFactory.CreateIndividual(passport, contact, individualAccount);
                            break;

                        case (int)ClientType.Entity:

                            //Данные аккаунта
                            card = EntitiesCardFactories[random.Next(EntitiesCardFactories.Length)].CreateCard(cardBalance);
                            deposit = random.Next(2) == 0 ? 
                                            new DefaultDepositFactory().CreateDeposit(depositBalance, capitalization, ClientType.Entity) : 
                                            new NullDeposit();

                            var entityAccount = AccountFactories[random.Next(AccountFactories.Length)].CreateAccount(card, deposit);

                            //Данные компании
                            var company = CompanyFactory.CreateCompany($"Компания_{i}", $"http:\\Company.Website_{i}.ru");

                            //Создание юр.лица
                            client = EntityFactory.CreateEntity(passport, contact, entityAccount, company);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    ProcessingCountEvent?.Invoke(Count++);
                    dbContext.Add(client);
                }
            });

            dbContext.SaveChanges();
        }
    }
}
