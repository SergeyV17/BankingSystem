namespace DataLibrary.Deposits
{
    /// <summary>
    /// Класс депозита
    /// </summary>
    public class DefaultDeposit : Deposit
    {
        /// <summary>
        /// Конструктор депозита по умолчанию
        /// </summary>
        /// <param name="depositBalance">баланс</param>
        /// <param name="depositCapitalization">капитализация</param>
        /// <param name="depositRate">ставка</param>
        public DefaultDeposit(decimal depositBalance, bool depositCapitalization, double depositRate) : base(depositBalance, depositCapitalization, depositRate) { }

        /// <summary>
        /// Метод возвращаюший состояние объекта
        /// </summary>
        /// <returns>булевая переменная</returns>
        public override bool IsNull() => false;
    }
}
