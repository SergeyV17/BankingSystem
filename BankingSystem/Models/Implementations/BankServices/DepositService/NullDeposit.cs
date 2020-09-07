namespace BankingSystem.Models.Implementations.BankServices.DepositService
{
    /// <summary>
    /// Класс нуль объекта депозита
    /// </summary>
    class NullDeposit : Deposit
    {
        /// <summary>
        /// Конструктор депозита по умолчанию
        /// </summary>
        public NullDeposit() : base(default, default, default) { }

        /// <summary>
        /// Метод возвращаюший состояние объекта
        /// </summary>
        /// <returns>булевая переменная</returns>
        public override bool IsNull() => true;
    }
}
