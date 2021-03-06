﻿using System;
using RequisitesLibrary.ClientRequisites.ContactData;

namespace RequisitesLibrary.ClientRequisites.Factories
{
    /// <summary>
    /// Класс фабрики номера телефона
    /// </summary>
    public static class PhoneNumberFactory
    {
        public const string countryCode = "+7";
        private const int maxIdNumber = 9999999;

        private static readonly int[] regionNumbers;
        private static int idNumber;
        private static int regionNumberCount;

        /// <summary>
        /// Конструктор фабрики номера телефона
        /// </summary>
        static PhoneNumberFactory()
        {
            regionNumbers = new[] { 877, 387, 388, 416, 818, 851, 347, 472, 072, 483, 083, 301,
                                     492,092, 844, 813, 820, 473, 073, 872, 426, 343, 493, 093};
        }

        /// <summary>
        /// Метод создание случайного номера телефона
        /// </summary>
        /// <returns>номер телефона</returns>
        public static PhoneNumber CreateNumber()
        {
            if (idNumber == maxIdNumber)
            {
                regionNumberCount++;
                idNumber = default;
            }

            idNumber++;

            return new PhoneNumber($"{countryCode}{regionNumbers[regionNumberCount]}{idNumber:D7}");
        }

        /// <summary>
        /// Метод создания номера телефона
        /// </summary>
        /// <param name="phoneNumber">номер телефона</param>
        /// <returns>номер телефона</returns>
        public static PhoneNumber CreateNumber(string phoneNumber)
        {
            if (!long.TryParse(phoneNumber, out _))
                throw new ArgumentException($"Передача недопустимого аргумента в параметры. Проверьте: {nameof(phoneNumber)}", phoneNumber);

            return new PhoneNumber($"{countryCode}{phoneNumber}");
        }
    }
}
