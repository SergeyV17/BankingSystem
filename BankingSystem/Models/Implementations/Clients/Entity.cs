using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites;

namespace BankingSystem.Models.Implementations.Clients
{
    class Entity : Client
    {
        public Entity(FullName fullName, string address, PhoneNumber phoneNumber, string email, Passport passport, Account account) 
            : base(fullName, address, phoneNumber, email, passport, account)
        {
        }
    }
}
