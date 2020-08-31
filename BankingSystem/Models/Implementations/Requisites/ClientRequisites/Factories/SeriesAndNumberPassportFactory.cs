using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;

namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.Factories
{
    static class SeriesAndNumberPassportFactory
    {
        private const int StartNumber = 000000;
        private const int MaxNumber = 999999;

        private static int _uniqueSeries;
        private static int _uniqueNumber;

        static SeriesAndNumberPassportFactory()
        {
            _uniqueSeries = 0000;
            _uniqueNumber = 000000;
        }

        /// <summary>
        /// Создание паспорта
        /// </summary>
        /// <returns>паспорт</returns>
        public static SeriesAndNumber CreatePassport()
        {
            if (_uniqueNumber == MaxNumber)
            {
                _uniqueSeries++;
                _uniqueNumber = StartNumber;
            }

            _uniqueNumber++;

            return new SeriesAndNumber(_uniqueSeries.ToString("D4"), _uniqueNumber.ToString("D6"));
        }
    }
}
