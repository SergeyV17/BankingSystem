using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.CompanyData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;

namespace BankingSystem.Models.Implementations.Clients
{
    class Entity : Client
    {
        public Company Company { get; }

        public Entity(Passport passport, Contact contact, Account account, Company company) : base(passport, contact, account)
        {
            Company = company;
        }
    }
}
