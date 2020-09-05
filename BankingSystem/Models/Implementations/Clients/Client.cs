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
        /// Конструктор по умолчаю для EF
        /// </summary>
        public Client() { }

        /// <summary>
        /// Контруктор клиента
        /// </summary>
        /// <param name="passport">пасспортные данные</param>
        /// <param name="contact">контактные данные</param>
        /// <param name="account">аккаунт</param>
        protected Client( Passport passport, Contact contact, Account account )
        {
            Passport = passport;
            Contact = contact;
            Account = account;
        }

        public int ClientId { get; set; }
        public Passport Passport { get; private set; }
        public Contact Contact { get; private set; }
        public Account Account { get; private set; }
    }
}
