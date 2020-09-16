using AccountsLibrary;
using RequisitesLibrary.ClientRequisites.CompanyData;
using RequisitesLibrary.ClientRequisites.ContactData;
using RequisitesLibrary.ClientRequisites.PassportData;

namespace ClientsLibrary.Factories
{
    /// <summary>
    /// Фабрика юр. лиц
    /// </summary>
    class EntityFactory
    {
        /// <summary>
        /// Метод создания юр. лица
        /// </summary>
        /// <param name="passport">пасспортные данные</param>
        /// <param name="contact">контактные данные</param>
        /// <param name="account">аккаунт</param>
        /// <param name="company">данные о компании</param>
        /// <returns>юр. лицо</returns>
        public static Entity CreateEntity(Passport passport, Contact contact, Account account, Company company) => new Entity(passport, contact, account, company);
    }
}
