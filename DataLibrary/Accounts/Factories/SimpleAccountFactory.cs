using DataLibrary.Cards;
using DataLibrary.Deposits;

namespace DataLibrary.Accounts.Factories
{
    /// <summary>
    /// Класс создания аккаунта в зависимости от типа аккаунта клиента
    /// </summary>
    public class SimpleAccountFactory
    {
        /// <summary>
        /// Метод создания аккаунта по типу
        /// </summary>
        /// <param name="type">тип аккаунта</param>
        /// <param name="card">карта</param>
        /// <param name="deposit">депозит</param>
        /// <returns>аккаунт</returns>
        public static Account CreateAccount(AccountType type, Card card, Deposit deposit)
        {
            Account account = null;

            switch (type)
            {
                case AccountType.Regular:
                    account = new RegularAccountFactory().CreateAccount(card, deposit);
                    break;
                case AccountType.VIP:
                    account = new VipAccountFactory().CreateAccount(card, deposit);
                    break;
            }

            return account;
        }
    }
}
