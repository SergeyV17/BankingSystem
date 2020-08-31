namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData
{
    readonly struct Passport
    {
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