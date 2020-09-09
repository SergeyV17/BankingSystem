using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Accounts.Factories;
using BankingSystem.Models.Implementations.BankServices.CardService.Factories;
using BankingSystem.Models.Implementations.BankServices.DepositService;
using BankingSystem.Models.Implementations.Clients.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData.Factories;

namespace BankingSystem.Models.Implementations.Data.DbInteraction
{
    /// <summary>
    /// Класс добавления клиентов в БД
    /// </summary>
    static class AddClient
    {
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
        public static (bool successfully, string message) AddIndividualToDb(string lastName, string firstName, string middleName, 
            string series, string number, 
            string address, 
            string phoneNumber, string email, 
            string cardName, AccountType accountType)
        {
            using (AppDbContext context = new AppDbContext())
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

                var individual = IndividualFactory.CreateIndividual(passport, contact, account);

                //Проверка на совпадения в реквизитах
                var (IsntMached, message) = SearchForMatches.ErrorProcessing(context, passport, contact);

                if (IsntMached)
                {
                    context.Clients.Add(individual);
                    context.SaveChanges();
                }

                return (IsntMached, message);
            }
        }

        public static void AddEntityToDb()
        {
            using (AppDbContext context = new AppDbContext())
            {
                //context.Clients.Add();
                //context.SaveChanges();
            }
        }
    }
}
