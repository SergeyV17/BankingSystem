namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites
{
    readonly struct Passport
    {
        public string Series { get; }
        public string Number { get; }

        public Passport(string series, string number)
        {
            this.Series = series;
            this.Number = number;
        }
    }
}
