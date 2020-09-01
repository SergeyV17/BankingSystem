using System;
using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.CompanyData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;

namespace BankingSystem.Models.Implementations.Clients.Factories
{
    class EntityFactory
    {
        public static Entity CreateEntity(Passport passport, Contact contact, Account account, Company company)
        {
            return new Entity(passport, contact, account, company);
        }
    }
}
