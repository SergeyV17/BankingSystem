﻿using System;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData;

namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.Factories
{
    /// <summary>
    /// Класс фабрики номера телефона
    /// </summary>
    static class PhoneNumberFactory
    {
        private const int _countryCode = 7;
        private const int _maxIdNumber = 9999999;

        private static readonly int[] _regionNumbers;
        private static int _idNumber;
        private static int _regionNumberCount;

        /// <summary>
        /// Конструктор фабрики номера телефона
        /// </summary>
        static PhoneNumberFactory()
        {
            _regionNumbers = new[] { 877, 387, 388, 416, 818, 851, 347, 472, 072, 483, 083, 301,
                                     492,092, 844, 813, 820, 473, 073, 872, 426, 343, 493, 093};
        }

        /// <summary>
        /// Метод создание случайного номера телефона
        /// </summary>
        /// <returns>номер телефона</returns>
        public static PhoneNumber CreateNumber()
        {
            if (_idNumber == _maxIdNumber)
            {
                _regionNumberCount++;
                _idNumber = default;
            }

            _idNumber++;

            return new PhoneNumber($"+{_countryCode}{_regionNumbers[_regionNumberCount]}{_idNumber:D7}");
        }

        /// <summary>
        /// Метод создания номера телефона
        /// </summary>
        /// <param name="phoneNumber">номер телефона</param>
        /// <returns>номер телефона</returns>
        public static PhoneNumber CreateNumber(string phoneNumber)
        {
            if (!int.TryParse(phoneNumber, out _))
                throw new ArgumentException($"Передача недопустимого аргумента в параметры. Проверьте: {nameof(phoneNumber)}", phoneNumber);

            return new PhoneNumber(phoneNumber);
        }
    }
}