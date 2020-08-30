namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.Factories
{
    static class PassportFactory
    {
        private const int StartNumber = 000000;
        private const int MaxNumber = 999999;

        private static int _uniqueSeries;
        private static int _uniqueNumber;

        static PassportFactory()
        {
            _uniqueSeries = 0000;
            _uniqueNumber = 000000;
        }

        /// <summary>
        /// Создание паспорта
        /// </summary>
        /// <returns>паспорт</returns>
        public static Passport CreatePassport()
        {
            if (_uniqueNumber == MaxNumber)
            {
                _uniqueSeries++;
                _uniqueNumber = StartNumber;
            }

            _uniqueNumber++;

            return new Passport(_uniqueSeries.ToString("D4"), _uniqueNumber.ToString("D6"));
        }
    }
}
