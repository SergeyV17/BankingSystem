using System;

namespace BankingSystem.Models.Implementations.Requisites.DepositRequisites.Factories
{
    static class DepositNumberFactory
    {
        private static readonly Random _random = new Random();

        /// <summary>
        /// Метод создания номера для вклада
        /// </summary>
        /// <returns>номер вклада</returns>
        public static string CreateDepositNumber()
        {
            return $"{_random.Next(1000000, 9999999)}";
        }
    }
}
