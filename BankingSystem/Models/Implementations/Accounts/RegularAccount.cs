using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.BankServices.DepositService;

namespace BankingSystem.Models.Implementations.Accounts
{
    class RegularAccount : Account
    {
        public RegularAccount(Card card, IDeposit deposit, bool accountLockout, decimal amountOfReplenishmentPerDay) 
            : base(card, deposit, accountLockout, amountOfReplenishmentPerDay)
        {
        }
    }
}
