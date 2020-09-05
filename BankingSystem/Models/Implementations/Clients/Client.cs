using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;

namespace BankingSystem.Models.Implementations.Clients
{
    /// <summary>
    /// класс клиента
    /// </summary>
    abstract class Client
    {
        /// <summary>
        /// Контруктор клиента
        /// </summary>
        /// <param name="passport">пасспортные данные</param>
        /// <param name="contact">контактные данные</param>
        /// <param name="account">аккаунт</param>
        protected Client( Passport passport, Contact contact, Account account)
        {
            Passport = passport;
            Contact = contact;
            Account = account;
        }

        public Passport Passport { get; }
        public Contact Contact { get; }
        public Account Account { get; }
    }
}
