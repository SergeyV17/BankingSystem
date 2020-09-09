using System;

namespace BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories
{
    /// <summary>
    /// Класс фабрики номера карты
    /// </summary>
    static class CardNumberFactory
    {
        private static readonly Random random = new Random();

        public static string CreateCardNumber() => $"{random.Next(1000, 9999)} {random.Next(1000, 9999)} {random.Next(1000, 9999)} {random.Next(1000, 9999)}";
    }
}
