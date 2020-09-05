using System;

namespace BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories
{
    /// <summary>
    /// Класс фабрики номера карты
    /// </summary>
    static class CardNumberFactory
    {
        private static readonly Random _random = new Random();

        public static string CreateCardNumber() => $"{_random.Next(1000, 9999)} {_random.Next(1000, 9999)} {_random.Next(1000, 9999)} {_random.Next(1000, 9999)}";
    }
}
