using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.BankServices.DepositService;

namespace BankingSystem.Models.Implementations.Accounts
{
    /// <summary>
    /// Класс вип аккаунта
    /// </summary>
    class VipAccount : Account
    {
        private const int DepositRateIncrease = 2; // Повышение ставки депозита х2

        /// <summary>
        /// Конструктор вип аккаута
        /// </summary>
        /// <param name="card">карта</param>
        /// <param name="deposit">депозит</param>
        public VipAccount(Card card, IDeposit deposit) : base(card, deposit)
        {
            Deposit.DepositRate *= DepositRateIncrease;
        }
    }
}
