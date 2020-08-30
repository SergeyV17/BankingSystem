using System;
using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.Factories;

namespace BankingSystem.Models.Implementations.Clients.Factories
{
    class ClientFactory
    {
        public Client CreateClient(ClientType clientType, Gender gender,
            string address, string email, Account account)
        {
            switch (clientType)
            {
                case ClientType.Individual:
                    return new Individual(
                        FullNameFactory.CreateFullName(gender),
                        address,
                        PhoneNumberFactory.CreateNumber(),
                        email,
                        PassportFactory.CreatePassport(),
                        account);
                case ClientType.Entity:
                    return new Entity(
                        FullNameFactory.CreateFullName(gender),
                        address,
                        PhoneNumberFactory.CreateNumber(),
                        email,
                        PassportFactory.CreatePassport(),
                        account);
                default:
                    throw new ArgumentOutOfRangeException(nameof(clientType), clientType, null);
            }
        }

        public Client CreateClient(ClientType clientType, 
            string lastName, string firstName, string middleName, string address, string phoneNumber, string email, Account account)
        {
            switch (clientType)
            {
                case ClientType.Individual:
                    return new Individual(
                        FullNameFactory.CreateFullName(lastName, firstName, middleName), 
                        address,
                        PhoneNumberFactory.CreateNumber(phoneNumber),
                        email,
                        PassportFactory.CreatePassport(),
                        account);
                case ClientType.Entity:
                    return new Entity(
                        FullNameFactory.CreateFullName(lastName, firstName, middleName),
                        address,
                        PhoneNumberFactory.CreateNumber(phoneNumber),
                        email,
                        PassportFactory.CreatePassport(),
                        account);
                default:
                    throw new ArgumentOutOfRangeException(nameof(clientType), clientType, null);
            }
        }
    }
}