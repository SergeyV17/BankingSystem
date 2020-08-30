using System;

namespace BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories
{
    static class CardNumberFactory
    {
        private static readonly Random _random = new Random();

        public static string CreateCardNumber()
        {
            return $"{_random.Next(1000, 9999)} {_random.Next(1000, 9999)} {_random.Next(1000, 9999)} {_random.Next(1000, 9999)}";
        }
    }
}
