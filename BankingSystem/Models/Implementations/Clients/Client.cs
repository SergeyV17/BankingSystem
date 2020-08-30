using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites;

namespace BankingSystem.Models.Implementations.Clients
{
    abstract class Client
    {
        public FullName FullName { get; }
        public string Address { get; }
        public PhoneNumber PhoneNumber { get; }
        public string Email { get; }
        public Passport Passport { get; }
        public Account Account { get;  }

        protected Client(FullName fullName, string address, PhoneNumber phoneNumber, string email, Passport passport, Account account)
        {
            FullName = fullName;
            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            Passport = passport;
            Account = account;
        }
    }
}
