namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData
{
    /// <summary>
    /// Структура серии и номера паспорта
    /// </summary>
    readonly struct SeriesAndNumber
    {
        /// <summary>
        /// Конструктор серии и номера паспорта
        /// </summary>
        /// <param name="series">серия</param>
        /// <param name="number">номер</param>
        public SeriesAndNumber(string series, string number)
        {
            this.Series = series;
            this.Number = number;
        }

        public string Series { get; }
        public string Number { get; }
    }
}
