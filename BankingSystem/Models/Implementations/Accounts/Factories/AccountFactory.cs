using System;
using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.BankServices.DepositService;

namespace BankingSystem.Models.Implementations.Accounts.Factories
{
    class AccountFactory
    {
        public static Account CreateAccount(AccountType accountType, Card card, Deposit deposit)
        {
            switch (accountType)
            {
                case AccountType.Regular:
                    return new RegularAccount(card, deposit, default, default);
                case AccountType.VIP:
                    return new VipAccount(card, deposit, default, default);
                default:
                    throw new ArgumentOutOfRangeException(nameof(accountType), accountType, null);
            }
        }
    }
}
