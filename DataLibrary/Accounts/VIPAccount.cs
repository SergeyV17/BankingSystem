using DataLibrary.Cards;
using DataLibrary.Deposits;

namespace DataLibrary.Accounts
{
    /// <summary>
    /// Класс вип аккаунта
    /// </summary>
    public class VIPAccount : Account
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
