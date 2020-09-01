using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.BankServices.DepositService;

namespace BankingSystem.Models.Implementations.Accounts
{
    class VipAccount : Account
    {
        private const int DepositRateIncrease = 2;

        public VipAccount(Card card, IDeposit deposit) : base(card, deposit)
        {
            Deposit.DepositRate *= DepositRateIncrease;
        }
    }
}
