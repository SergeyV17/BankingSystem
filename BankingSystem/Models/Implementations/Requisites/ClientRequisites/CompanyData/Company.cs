namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.CompanyData
{
    readonly struct Company
    {
        public Company(string name, string website)
        {
            Name = name;
            Website = website;
        }

        public string Name { get; }
        public string Website { get;  }
    }
}