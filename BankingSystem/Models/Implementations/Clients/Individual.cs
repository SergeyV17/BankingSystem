using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;

namespace BankingSystem.Models.Implementations.Clients
{
    /// <summary>
    /// Класс физ. лица
    /// </summary>
    class Individual : Client
    {
        public Individual()
        {

        }

        /// <summary>
        /// Конструктор физ. лца
        /// </summary>
        /// <param name="passport">пасспортные данные</param>
        /// <param name="contact">контактные данные</param>
        /// <param name="account">аккаунт</param>
        public Individual(Passport passport, Contact contact, Account account) : base(passport, contact, account)
        {
        }
    }
}
