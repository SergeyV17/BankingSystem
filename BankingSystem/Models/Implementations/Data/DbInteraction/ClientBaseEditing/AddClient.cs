using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Accounts.Factories;
using BankingSystem.Models.Implementations.BankServices.CardService.Factories;
using BankingSystem.Models.Implementations.BankServices.DepositService;
using BankingSystem.Models.Implementations.Clients.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.CompanyData.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData;
using BankingSystem.Models.Implementations.Data.DbInteraction.SearchesForMatches;

namespace BankingSystem.Models.Implementations.Data.DbInteraction.ClientBaseEditing
{
    /// <summary>
    /// Класс добавления клиентов в БД
    /// </summary>
    static class AddClient
    {
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
            var card = CardFactory.CreateCard(cardName, default);
            var deposit = new NullDeposit();
            var account = AccountFactory.CreateAccount(accountType, card, deposit);

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
                    message = SuccessMessage(passport.FullName.Name);
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
                    message = SuccessMessage(passport.FullName.Name);
                }

                return (noMatchesFound, message);
            }
        }

        /// <summary>
        /// Метод порождающий сообщение об успешном добавлении клиента
        /// </summary>
        /// <param name="clientName">полное имя клиента</param>
        /// <returns>сообщение</returns>
        private static string SuccessMessage(string clientName)
        {
            return $"Клиент \"{clientName}\" успешно добавлен.";
        }
    }
}
