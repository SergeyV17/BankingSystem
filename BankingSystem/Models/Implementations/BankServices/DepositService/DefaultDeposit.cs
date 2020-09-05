namespace BankingSystem.Models.Implementations.BankServices.DepositService
{
    /// <summary>
    /// Класс депозита
    /// </summary>
    class DefaultDeposit : Deposit
    {
        /// <summary>
        /// Конструктор депозита по умолчанию
        /// </summary>
        /// <param name="depositBalance">баланс</param>
        /// <param name="depositCapitalization">капитализация</param>
        /// <param name="depositRate">ставка</param>
        public DefaultDeposit(decimal depositBalance, bool depositCapitalization, decimal depositRate) : base(depositBalance, depositCapitalization, depositRate) { }
    }
}
