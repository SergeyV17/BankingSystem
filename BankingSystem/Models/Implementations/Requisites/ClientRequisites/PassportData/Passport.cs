namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData
{
    /// <summary>
    /// Структура паспортных данных
    /// </summary>
    readonly struct Passport
    {
        /// <summary>
        /// Конструктор паспортных данных
        /// </summary>
        /// <param name="fullName">ФИО</param>
        /// <param name="seriesAndNumber">серия и номер</param>
        /// <param name="address">адрес</param>
        public Passport(FullName fullName, SeriesAndNumber seriesAndNumber, string address)
        {
            FullName = fullName;
            SeriesAndNumber = seriesAndNumber;
            Address = address;
        }

        public FullName FullName { get; }
        public string Address { get; }
        public SeriesAndNumber SeriesAndNumber { get; }
    }
}