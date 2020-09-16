using DataLibrary.Accounts;
using RequisitesLibrary.ClientRequisites.ContactData;
using RequisitesLibrary.ClientRequisites.PassportData;

namespace DataLibrary.Clients
{
    /// <summary>
    /// Класс физ. лица
    /// </summary>
    public class Individual : Client
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
