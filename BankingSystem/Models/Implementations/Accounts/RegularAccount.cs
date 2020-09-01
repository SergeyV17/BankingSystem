using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.BankServices.DepositService;

namespace BankingSystem.Models.Implementations.Accounts
{
    class RegularAccount : Account
    {
        public RegularAccount(Card card, IDeposit deposit) : base(card, deposit)
        {
        }
    }
}
