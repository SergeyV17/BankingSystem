namespace DataLibrary.Deposits
{
    /// <summary>
    /// Класс нуль объекта депозита
    /// </summary>
    public class NullDeposit : Deposit
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
