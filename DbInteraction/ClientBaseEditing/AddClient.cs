using System;
using RequisitesLibrary.ClientRequisites.PassportData;
using RequisitesLibrary.ClientRequisites.ContactData;
using DataLibrary.Accounts;
using RequisitesLibrary.PassportData.Factories;
using RequisitesLibrary.ClientRequisites.PassportData.Factories;
using RequisitesLibrary.ClientRequisites.Factories;
using RequisitesLibrary.ClientRequisites.ContactData.Factories;
using DataLibrary.Cards.Factories;
using DataLibrary.Deposits;
using DataLibrary.Accounts.Factories;
using DataLibrary.Clients.Factories;
using RequisitesLibrary.ClientRequisites.CompanyData.Factories;
using DbInteraction.ClientBaseEditing.EventArgs;
using DbInteraction.SearchesForMatches;

namespace DbInteraction.ClientBaseEditing
{
    /// <summary>
    /// Класс добавления клиентов в БД
    /// </summary>
    public class AddClient
    {
        public static event EventHandler<AddClientEventArgs> ClientAdded;

        /// <summary>
        /// Метод создания базовых реквизитов
        /// </summary>
        /// <param name="lastName">фамилия</param>
        /// <param name="firstName">имя</param>
        /// <param name="middleName">отчество</param>
        /// <param name="series">серия</param>
        /// <param name="number">номер</param>
        /// <param name="address">адрес</param>
        /// <param name="phoneNumber">номер телефона</param>
        /// <param name="email">эмейл</param>
        /// <param name="cardName">наименование карты</param>
        /// <param name="accountType">тип аккаунта</param>
        /// <returns>паспортные данные, контактные данные, аккаунт</returns>
        private static (Passport passport, Contact contact, Account account) CreateBaseRequisites(string lastName, string firstName, string middleName,
            string series, string number,
            string address,
            string phoneNumber, string email,
            string cardName, AccountType accountType)
        {   
            //Паспортные данные
            var fullName = FullNameFactory.CreateFullName(lastName, firstName, middleName);
            var seriesAndNumber = SeriesAndNumberFactory.CreateSeriesAndNumber(series, number);
            var passport = PassportFactory.CreatePassport(fullName, seriesAndNumber, address);

            //Контактные данные
            var phone = PhoneNumberFactory.CreateNumber(phoneNumber);
            var contact = ContactFactory.CreateContact(phone, email);

            //Данные аккаунта
            var card = SimpleCardFactory.CreateCard(cardName, default);
            var deposit = new NullDeposit();
            var account = SimpleAccountFactory.CreateAccount(accountType, card, deposit);

            return (passport, contact, account);
        }

        /// <summary>
        /// Метод добавления физ.лица в БД
        /// </summary>
        /// <param name="lastName">фамилия</param>
        /// <param name="firstName">имя</param>
        /// <param name="middleName">отчество</param>
        /// <param name="series">серия паспорта</param>
        /// <param name="number">номер паспорта</param>
        /// <param name="address">адрес</param>
        /// <param name="phoneNumber">номер телефона</param>
        /// <param name="email">эмейл</param>
        /// <param name="cardName">наименование карты</param>
        /// <param name="accountType">тип аккаунта</param>
        /// <returns>признак успешного добавления, сообщение</returns>
        public static (bool successfully, string message) AddIndividualToDb(string lastName, string firstName, string middleName, 
            string series, string number, 
            string address, 
            string phoneNumber, string email, 
            string cardName, AccountType accountType)
        {
            using (AppDbContext context = new AppDbContext())
            {
                var (passport, contact, account) = CreateBaseRequisites(lastName, firstName, middleName, series, number, address, phoneNumber, email, cardName, accountType);

                //Создание физ.лица
                var individual = IndividualFactory.CreateIndividual(passport, contact, account);

                //Проверка на совпадения в реквизитах
                var (noMatchesFound, message) = SearchForMatchesForAdding.IndividualErrorProcessing(passport, contact);

                if (noMatchesFound)
                {
                    context.Clients.Add(individual);

                    context.SaveChanges();
                    message = "Произведена операция добавления:\n" +
                               $"Клиент: {individual.Passport.FullName.Name}\n" +
                               $"Карта: {individual.Account.Card.CardName}\n" +
                               $"Номер: {individual.Account.Card.CardNumber}\n" +
                               $"Статус: {(individual.Account is RegularAccount ? "Стандарт" : "VIP")}\n" +
                               $"Дата: {DateTime.Now: dd/MM/yyyy HH:mm:ss}\n" +
                               "Отчет: Успешно";

                    ClientAdded?.Invoke(null, new AddClientEventArgs { LogMessage = message });
                }

                return (noMatchesFound, message);
            }
        }

        /// <summary>
        /// Метод добавления юр.лица в БД
        /// </summary>
        /// <param name="lastName">фамилия</param>
        /// <param name="firstName">имя</param>
        /// <param name="middleName">отчество</param>
        /// <param name="series">серия паспорта</param>
        /// <param name="number">номер паспорта</param>
        /// <param name="address">адрес</param>
        /// <param name="phoneNumber">номер телефона</param>
        /// <param name="email">эмейл</param>
        /// <param name="cardName">наименование карты</param>
        /// <param name="accountType">тип аккаунта</param>
        /// <returns>признак успешного добавления, сообщение</returns>
        public static (bool successfully, string message) AddEntityToDb(string lastName, string firstName, string middleName,
            string series, string number,
            string address,
            string phoneNumber, string email,
            string nameOfCompany, string website,
            string cardName, AccountType accountType)
        {
            using (AppDbContext context = new AppDbContext())
            {
                var (passport, contact, account) = CreateBaseRequisites(lastName, firstName, middleName, series, number, address, phoneNumber, email, cardName, accountType);

                //Данные компании
                var company = CompanyFactory.CreateCompany(nameOfCompany, website);

                //Создание юр.лица
                var entity = EntityFactory.CreateEntity(passport, contact, account, company);

                //Проверка на совпадения в реквизитах
                var (noMatchesFound, message) = SearchForMatchesForAdding.EntityErrorProcessing(passport, contact, company);

                if (noMatchesFound)
                {
                    context.Clients.Add(entity);

                    context.SaveChanges();
                    message = "Произведена операция добавления:\n" +
                                $"Клиент: {entity.Passport.FullName.Name}\n" +
                                $"Карта: {entity.Account.Card.CardName}\n" +
                                $"Номер: {entity.Account.Card.CardNumber}\n" +
                                $"Статус: {(entity.Account is RegularAccount ? "Стандарт" : "VIP")}\n" +
                                $"Дата: {DateTime.Now : dd/MM/yyyy HH:mm:ss}\n" +
                                "Отчет: Успешно";

                    ClientAdded?.Invoke(null, new AddClientEventArgs { LogMessage = message });
                }

                return (noMatchesFound, message);
            }
        }
    }
}
