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
    class RepositoryFactory
    {
        private  readonly Random random;

        private  readonly AccountFactory[] accountFactories;
        private  readonly CardFactory[] individualsCardFactories;
        private readonly CardFactory[] entitiesCardFactories;
        public RepositoryFactory()
        {
            random = new Random();

            // массивы классов фабричного метода, для возможности выбора рандомной фабрики
            accountFactories = new AccountFactory[]
            {
                new RegularAccountFactory(), new VIPAccountFactory() // классы фабричного метода аккаунта
            };

            individualsCardFactories = new CardFactory[]
            {
                new VisaClassicFactory(), new VisaBlackFactory(), new VisaPlatinumFactory() // классы фабричного метода карты ( для физ лиц)
            };

            entitiesCardFactories = new CardFactory[]{ new VisaCorporateFactory()}; // класс фабричного метода карты ( для юр лиц)
        }

        public  Repository CreateRepository(AppDbContext dbContext, int quantity)
        {
            var root = CreateRepositoryTree();

            if (!dbContext.Clients.Any())
            {
                FillRepository(dbContext, quantity);
            }

            return Repository.GetRepository(root);
        }

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
                        client = IndividualFactory.CreateIndividual(
                           passport,
                            ContactFactory.CreateContact(
                                PhoneNumberFactory.CreateNumber(),
                                $"Client@Email.ru_{i}"),
                            accountFactories[random.Next(accountFactories.Length)].CreateAccount(
                                individualsCardFactories[random.Next(individualsCardFactories.Length)]
                                    .CreateCard(balance),
                                new DefaultDepositFactory().CreateDeposit(balance, capitalization,
                                    ClientType.Individual)
                            ));
                        break;
                    case (int)ClientType.Entity:
                        client = EntityFactory.CreateEntity(
                            passport,
                            ContactFactory.CreateContact(
                                PhoneNumberFactory.CreateNumber(),
                                $"Company@Email.ru_{i}"),
                            accountFactories[random.Next(accountFactories.Length)].CreateAccount(
                                individualsCardFactories[random.Next(entitiesCardFactories.Length)].CreateCard(balance),
                                new DefaultDepositFactory().CreateDeposit(balance, capitalization,
                                    ClientType.Individual)),
                            new Company(
                                $"Company_{i}",
                                $"Company.Website.ru_{i}")
                        );
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
