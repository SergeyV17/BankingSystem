using DataLibrary.Cards;
using DataLibrary.Deposits;

namespace DataLibrary.Accounts
{
    /// <summary>
    /// Класс стандартного аккаунта
    /// </summary>
    public class RegularAccount : Account
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
