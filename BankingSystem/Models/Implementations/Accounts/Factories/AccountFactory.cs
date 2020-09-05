using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.BankServices.DepositService;

namespace BankingSystem.Models.Implementations.Accounts.Factories
{
    /// <summary>
    /// Класс фабрики аккаунтов
    /// </summary>
    abstract class AccountFactory
    {
        /// <summary>
        /// Метод создания аккаунта
        /// </summary>
        /// <param name="card">карта</param>
        /// <param name="deposit">депозит</param>
        /// <returns>аккаунт</returns>
        public abstract Account CreateAccount(Card card, IDeposit deposit);
    }
}
