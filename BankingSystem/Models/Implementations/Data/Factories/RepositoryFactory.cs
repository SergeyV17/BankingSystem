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
        private readonly Random random;

        // массивы с фабриками для возможности выбора рандомной фабрики
        private readonly AccountFactory[] accountFactories;
        private readonly CardFactory[] individualsCardFactories;
        private readonly CardFactory[] entitiesCardFactories;

        /// <summary>
        /// Конструктор репозитория
        /// </summary>
        public RepositoryFactory()
        {
            random = new Random();

            accountFactories = new AccountFactory[]
            {
                new RegularAccountFactory(), new VIPAccountFactory()
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
        public  Repository CreateRepository(AppDbContext dbContext, int quantity)
        {
            var root = CreateRepositoryTree();

            if (!dbContext.Clients.Any())
            {
                FillRepository(dbContext, quantity);
            }

            return Repository.GetRepository(root);
        }

        /// <summary>
        /// Метод создания дерева для репозитория
        /// </summary>
        /// <returns>корневой узел</returns>
        private Node CreateRepositoryTree()
        {
            var root = new Node("Банковская система");

            var individual = new Node("Физические лица");
            var entity = new Node("Юридические лица");
            var VIPClients = new Node("Вип клиенты");

            root.AddNode(individual);
            root.AddNode(entity);
            root.AddNode(VIPClients);

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
        private void FillRepository(AppDbContext dbContext, int quantity)
        {
            Parallel.For(0, quantity, (i) =>
            {
                Client client = null;

                decimal balance = random.Next(1, 10) * 10000;
                bool capitalization = Convert.ToBoolean(random.Next(2));

                var passport = PassportFactory.CreatePassport(
                    FullNameFactory.CreateFullName(Gender.Female),
                    SeriesAndNumberFactory.CreateSeriesAndNumber(),
                    $"Address_{i}");


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

                        contact = ContactFactory.CreateContact(PhoneNumberFactory.CreateNumber(),$"Client@Email.ru_{i}");

                        var entityAccount = accountFactories[random.Next(accountFactories.Length)].CreateAccount(
                                entitiesCardFactories[random.Next(entitiesCardFactories.Length)].CreateCard(balance),
                                new DefaultDepositFactory().CreateDeposit(balance, capitalization, ClientType.Entity));

                        var company = new Company($"Company_{i}", $"Company.Website.ru_{i}");

                        client = EntityFactory.CreateEntity(passport, contact, entityAccount, company);

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                dbContext.Add(client);
            });

            dbContext.SaveChanges();
        }
    }
}
