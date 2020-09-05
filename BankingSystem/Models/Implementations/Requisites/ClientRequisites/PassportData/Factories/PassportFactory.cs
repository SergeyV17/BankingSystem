namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData.Factories
{
    /// <summary>
    /// Класс фабрики паспортных данных
    /// </summary>
    class PassportFactory
    {
        /// <summary>
        /// Метод создания паспортных данных
        /// </summary>
        /// <param name="fullName">ФИО</param>
        /// <param name="seriesAndNumber">серия и номер</param>
        /// <param name="address">адрес</param>
        /// <returns>паспортные данные</returns>
        public static Passport CreatePassport(FullName fullName, SeriesAndNumber seriesAndNumber, string address) => new Passport(fullName, seriesAndNumber, address);
    }
}
