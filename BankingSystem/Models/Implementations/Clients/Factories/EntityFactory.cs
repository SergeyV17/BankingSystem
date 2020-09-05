using System;
using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.CompanyData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;

namespace BankingSystem.Models.Implementations.Clients.Factories
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
