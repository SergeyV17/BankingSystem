using DataLibrary.Cards;
using DataLibrary.Deposits;

namespace DataLibrary.Accounts.Factories
{
    /// <summary>
    /// Класс фабрики стандартного аккаунта
    /// </summary>
    public class RegularAccountFactory : AccountFactory
    {
        /// <summary>
        /// Метод создания стандартного аккаунта
        /// </summary>
        /// <param name="card">карта</param>
        /// <param name="deposit">депозит</param>
        /// <returns>стандартный аккаунт</returns>
        public override Account CreateAccount(Card card, Deposit deposit) => new RegularAccount(card, deposit);
    }
}
