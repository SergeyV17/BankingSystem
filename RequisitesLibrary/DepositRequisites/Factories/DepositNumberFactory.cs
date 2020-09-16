using System;

namespace RequisitesLibrary.DepositRequisites.Factories
{
    /// <summary>
    /// Класс фабрики номера депозита
    /// </summary>
    public static class DepositNumberFactory
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// Метод создания номера депозита
        /// </summary>
        /// <returns>номер депозита</returns>
        public static string CreateDepositNumber() => $"{_random.Next(1000000, 9999999)}";
    }
}
