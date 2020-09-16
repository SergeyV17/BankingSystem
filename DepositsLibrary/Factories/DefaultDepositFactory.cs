using RequisitesLibrary;

namespace DepositsLibrary.Factories
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
        public override Deposit CreateDeposit(decimal depositBalance, bool depositCapitalization, ClientType clientType) => 
            new DefaultDeposit(depositBalance, depositCapitalization, DepositRates.GetDepositRate(clientType));
    }
}
