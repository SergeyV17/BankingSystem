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
        public abstract Account CreateAccount(Card card, Deposit deposit);

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
