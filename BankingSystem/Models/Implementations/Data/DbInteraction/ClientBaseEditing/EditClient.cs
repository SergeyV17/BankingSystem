using BankingSystem.Models.Implementations.Clients;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.CompanyData.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.Factories;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData.Factories;
using System.Linq;

namespace BankingSystem.Models.Implementations.Data.DbInteraction.ClientBaseEditing
{
    /// <summary>
    /// Класс редактирования клиентов БД
    /// </summary>
    class EditClient
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
        /// <returns>паспортные данные, контактные данные</returns>
        private static (Passport passport, Contact contact) CreateBaseRequisites(string lastName, string firstName, string middleName,
            string series, string number,
            string address,
            string phoneNumber, string email)
        {
            //Паспортные данные
            var fullName = FullNameFactory.CreateFullName(lastName, firstName, middleName);
            var seriesAndNumber = SeriesAndNumberFactory.CreateSeriesAndNumber(series, number);
            var passport = PassportFactory.CreatePassport(fullName, seriesAndNumber, address);

            //Контактные данные
            var phone = PhoneNumberFactory.CreateNumber(phoneNumber);
            var contact = ContactFactory.CreateContact(phone, email);

            return (passport, contact);
        }

        /// <summary>
        /// Метод редактирования физ.лица БД
        /// </summary>
        /// <param name="selectedIndividual">выбранный клиент</param>
        /// <param name="lastName">фамилия</param>
        /// <param name="firstName">имя</param>
        /// <param name="middleName">отчество</param>
        /// <param name="series">серия паспорта</param>
        /// <param name="number">номер паспорта</param>
        /// <param name="address">адрес</param>
        /// <param name="phoneNumber">номер телефона</param>
        /// <param name="email">эмейл</param>
        /// <param name="cardName">наименование карты</param>
        /// <returns>признак успешного редактирования, сообщение</returns>
        public static (bool successfully, string message) EditIndividualFromDb(Individual selectedIndividual, 
            string lastName, string firstName, string middleName,
            string series, string number, 
            string address, 
            string phoneNumber, string email, 
            string cardName)
        {
            using (AppDbContext context = new AppDbContext())
            {
                var individual = context.Clients.FirstOrDefault(c => c.Id == selectedIndividual.Id);
                var card = context.Cards.FirstOrDefault(c => c.Id == selectedIndividual.Id);

                var (passport, contact) = CreateBaseRequisites(lastName, firstName, middleName, series, number, address, phoneNumber, email);

                //Проверка на совпадения в реквизитах
                var (noMatchesFound, message) = SearchForMatches.IndividualErrorProcessing(context, passport, contact);

                if (noMatchesFound)
                {
                    individual.Passport = passport;
                    individual.Contact = contact;
                    card.CardName = cardName;

                    context.SaveChanges();
                    message = SuccessMessage(passport.FullName.Name);
                }

                return (noMatchesFound, message);
            }
        }

        /// <summary>
        /// Метод редактирования юр.лица БД
        /// </summary>
        /// <param name="selectedEntity">выбранный клиент</param>
        /// <param name="lastName">фамилия</param>
        /// <param name="firstName">имя</param>
        /// <param name="middleName">отчество</param>
        /// <param name="series">серия паспорта</param>
        /// <param name="number">номер паспорта</param>
        /// <param name="address">адрес</param>
        /// <param name="phoneNumber">номер телефона</param>
        /// <param name="email">эмейл</param>
        /// <param name="cardName">наименование карты</param>
        /// <returns>признак успешного редактирования, сообщение</returns>
        public static (bool successfully, string message) EditEntityFromDb(Entity selectedEntity,
            string lastName, string firstName, string middleName,
            string series, string number,
            string address,
            string phoneNumber, string email,
            string nameOfCompany, string website)
        {
            using (AppDbContext context = new AppDbContext())
            {
                var client = context.Clients.FirstOrDefault(c => c.Id == selectedEntity.Id);
                var entity = context.Entities.FirstOrDefault(e => e.Id == selectedEntity.Id);

                var (passport, contact) = CreateBaseRequisites(lastName, firstName, middleName, series, number, address, phoneNumber, email);

                //Данные компании
                var company = CompanyFactory.CreateCompany(nameOfCompany, website);

                //Проверка на совпадения в реквизитах
                var (noMatchesFound, message) = SearchForMatches.EntityErrorProcessing(context, passport, contact, company);

                if (noMatchesFound)
                {
                    client.Passport = passport;
                    client.Contact = contact;
                    entity.Company = company;

                    context.SaveChanges();
                    message = SuccessMessage(passport.FullName.Name);
                }

                return (noMatchesFound, message);
            }
        }

        /// <summary>
        /// Метод порождающий сообщение об успешном редактировании клиента
        /// </summary>
        /// <param name="clientName">полное имя клиента</param>
        /// <returns>сообщение</returns>
        private static string SuccessMessage(string clientName)
        {
            return $"Клиент \"{clientName}\" успешно отредактирован.";
        }
    }
}
