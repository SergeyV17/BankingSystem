using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites;

namespace BankingSystem.Models.Implementations.Clients
{
    class Individual : Client
    {
        public Individual(FullName fullName, string address, PhoneNumber phoneNumber, string email, Passport passport, Account account) 
            : base(fullName, address, phoneNumber, email, passport, account)
        {
        }
    }
}
