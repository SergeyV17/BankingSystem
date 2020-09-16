using DataLibrary.Cards;
using DataLibrary.Deposits;

namespace DataLibrary.Accounts.Factories
{
    /// <summary>
    /// Класс фабрики вип аккаунта
    /// </summary>
    public class VipAccountFactory : AccountFactory
    {
        /// <summary>
        /// Метод создания вип аккаунта
        /// </summary>
        /// <param name="card">карта</param>
        /// <param name="deposit">депозит</param>
        /// <returns>вип аккаунт</returns>
        public override Account CreateAccount(Card card, Deposit deposit) => new VIPAccount(card, deposit);
    }
}
