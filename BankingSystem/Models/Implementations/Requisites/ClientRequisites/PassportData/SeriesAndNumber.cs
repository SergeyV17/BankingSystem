namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData
{
    readonly struct SeriesAndNumber
    {
        public string Series { get; }
        public string Number { get; }

        public SeriesAndNumber(string series, string number)
        {
            this.Series = series;
            this.Number = number;
        }
    }
}
