namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData.Factories
{
    class PassportFactory
    {
        public static Passport CreatePassport(FullName fullName, SeriesAndNumber seriesAndNumber, string address)
        {
            return new Passport(fullName, seriesAndNumber, address);
        }
    }
}
