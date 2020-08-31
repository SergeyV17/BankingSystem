using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;

namespace BankingSystem.Models.Implementations.Clients
{
    abstract class Client
    {
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
