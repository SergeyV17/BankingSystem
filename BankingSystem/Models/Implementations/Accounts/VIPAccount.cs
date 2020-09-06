using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.BankServices.DepositService;

namespace BankingSystem.Models.Implementations.Accounts
{
    /// <summary>
    /// Класс вип аккаунта
    /// </summary>
    class VIPAccount : Account
    {
        private const int DepositRateIncrease = 2; // Повышение ставки депозита х2

        /// <summary>
        /// Конструктор по умолчанию для EF
        /// </summary>
        public VIPAccount() { }

        /// <summary>
        /// Конструктор вип аккаута
        /// </summary>
        /// <param name="card">карта</param>
        /// <param name="deposit">депозит</param>
        public VIPAccount(Card card, Deposit deposit) : base(card, deposit)
        {
            Deposit.DepositRate *= DepositRateIncrease;
        }
    }
}
