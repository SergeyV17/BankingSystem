using BankingSystem.Models.Implementations.Clients;

namespace BankingSystem.Models.Implementations.BankServices.DepositService.Factories
{
    /// <summary>
    /// Стандартная фабрика депозита
    /// </summary>
    class DefaultDepositFactory : DepositFactory
    {
        /// <summary>
        /// Метод создания депозита
        /// </summary>
        /// <param name="depositBalance">баланс</param>
        /// <param name="depositCapitalization">капитализация</param>
        /// <param name="clientType">тип клиента</param>
        /// <returns>депозит</returns>
        public override IDeposit CreateDeposit(decimal depositBalance, bool depositCapitalization, ClientType clientType) => 
            new Deposit(depositBalance, depositCapitalization, DepositRates.GetDepositRate(clientType));
    }
}
