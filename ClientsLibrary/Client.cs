using AccountsLibrary;
using RequisitesLibrary.ClientRequisites.ContactData;
using RequisitesLibrary.ClientRequisites.PassportData;

namespace ClientsLibrary
{
    /// <summary>
    /// класс клиента
    /// </summary>
    public abstract class Client
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

        public int Id { get; set; }
        public Passport Passport { get; set; }
        public Contact Contact { get; set; }
        public Account Account { get; private set; }
    }
}
