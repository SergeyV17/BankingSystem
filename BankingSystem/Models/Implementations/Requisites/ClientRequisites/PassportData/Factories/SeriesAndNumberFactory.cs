using System;
using System.Linq;

namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData.Factories
{
    /// <summary>
    /// Класс фабрики серии и номера паспорта
    /// </summary>
    static class SeriesAndNumberFactory
    {
        private const int StartNumber = 000000;
        private const int MaxNumber = 999999;

        private static int _uniqueSeries;
        private static int _uniqueNumber;

        /// <summary>
        /// Конструктор серии и номера паспорта
        /// </summary>
        static SeriesAndNumberFactory()
        {
            _uniqueSeries = 0000;
            _uniqueNumber = 000000;
        }

        /// <summary>
        /// Метод создания серии и номера паспорта
        /// </summary>
        /// <returns>серия и номер паспорта</returns>
        public static SeriesAndNumber CreateSeriesAndNumber()
        {
            if (_uniqueNumber == MaxNumber)
            {
                _uniqueSeries++;
                _uniqueNumber = StartNumber;
            }

            _uniqueNumber++;

            return new SeriesAndNumber(_uniqueSeries.ToString("D4"), _uniqueNumber.ToString("D6"));
        }

        /// <summary>
        /// Метод создания серии и номера паспорта
        /// </summary>
        /// <param name="series">серия</param>
        /// <param name="number">номер</param>
        /// <returns>серия и номер</returns>
        public static SeriesAndNumber CreateSeriesAndNumber(string series, string number)
        {
            if (!int.TryParse(series, out _) || !int.TryParse(number, out _))
                throw new ArgumentException($"Передача недопустимого аргумента в параметры. Проверьте: {nameof(series)} и {nameof(number)}");

            return new SeriesAndNumber(series, number);
        }
    }
}
