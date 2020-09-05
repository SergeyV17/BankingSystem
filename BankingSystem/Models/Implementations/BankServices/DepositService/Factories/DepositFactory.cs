using BankingSystem.Models.Implementations.Clients;

namespace BankingSystem.Models.Implementations.BankServices.DepositService.Factories
{
    /// <summary>
    /// Класс фабрики депозита
    /// </summary>
    abstract class DepositFactory
    {
        /// <summary>
        /// Метод создания депозита
        /// </summary>
        /// <param name="depositBalance">баланс</param>
        /// <param name="depositCapitalization">капитализация</param>
        /// <param name="clientType">тип клиента</param>
        /// <returns>депозит</returns>
        public abstract IDeposit CreateDeposit(decimal depositBalance, bool depositCapitalization, ClientType clientType);
    }
}
