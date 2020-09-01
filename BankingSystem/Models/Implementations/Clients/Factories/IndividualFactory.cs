using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;

namespace BankingSystem.Models.Implementations.Clients.Factories
{
    class IndividualFactory
    {   
        public static Individual CreateIndividual(Passport passport, Contact contact, Account account)
        {
            return new Individual(passport, contact, account);
        }
    }
}
