using DataLibrary.Accounts;
using RequisitesLibrary.ClientRequisites.CompanyData;
using RequisitesLibrary.ClientRequisites.ContactData;
using RequisitesLibrary.ClientRequisites.PassportData;

namespace DataLibrary.Clients
{
    /// <summary>
    /// Класс юр. лица
    /// </summary>
    public class Entity : Client
    {
        /// <summary>
        /// Конструктор по умолчанию для EF
        /// </summary>
        public Entity() { }

        /// <summary>
        /// Конструктор юр. лица
        /// </summary>
        /// <param name="passport">пасспортные данные</param>
        /// <param name="contact">контактные данные</param>
        /// <param name="account">аккаунт</param>
        /// <param name="company">данные о компании</param>
        public Entity(Passport passport, Contact contact, Account account, Company company) : base(passport, contact, account)
        {
            Company = company;
        }

        public Company Company { get; set; }
    }
}
