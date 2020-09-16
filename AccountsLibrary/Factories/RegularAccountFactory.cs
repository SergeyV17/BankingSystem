using CardsLibrary;
using DepositsLibrary;

namespace AccountsLibrary.Factories
{
    /// <summary>
    /// Класс фабрики стандартного аккаунта
    /// </summary>
    class RegularAccountFactory : AccountFactory
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
