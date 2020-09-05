using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.BankServices.DepositService;

namespace BankingSystem.Models.Implementations.Accounts
{
    /// <summary>
    /// Класс стандартного аккаунта
    /// </summary>
    class RegularAccount : Account
    {
        /// <summary>
        /// Конструктор по умолчанию для EF
        /// </summary>
        public RegularAccount() { }

        /// <summary>
        /// Конструктор стандартного аккаунта
        /// </summary>
        /// <param name="card">карта</param>
        /// <param name="deposit">депозит</param>
        public RegularAccount(Card card, Deposit deposit) : base(card, deposit) { }
    }
}
