using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.BankServices.DepositService;

namespace BankingSystem.Models.Implementations.Accounts
{
    abstract class Account
    {
        public Card Card { get; }
        public IDeposit Deposit { get; }
        public bool AccountLockout { get; }
        public decimal AmountOfReplenishmentPerDay { get; }
        public bool HasDeposit => Deposit.DepositNumber != null;

        protected Account(Card card, IDeposit deposit, bool accountLockout, decimal amountOfReplenishmentPerDay)
        {
            Card = card;
            Deposit = deposit;
            AccountLockout = accountLockout;
            AmountOfReplenishmentPerDay = amountOfReplenishmentPerDay;
        }
    }
}
