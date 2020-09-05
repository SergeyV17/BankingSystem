using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.BankServices.DepositService;

namespace BankingSystem.Models.Implementations.Accounts.Factories
{
    /// <summary>
    /// Класс фабрики вип аккаунта
    /// </summary>
    class VipAccountFactory : AccountFactory
    {
        /// <summary>
        /// Метод создания вип аккаунта
        /// </summary>
        /// <param name="card">карта</param>
        /// <param name="deposit">депозит</param>
        /// <returns>вип аккаунт</returns>
        public override Account CreateAccount(Card card, Deposit deposit) => new VipAccount(card, deposit);
    }
}
