namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites
{
    readonly struct FullName
    {
        public string LastName { get; }
        public string FirstName { get; }
        public string MiddleName { get; }

        public FullName(string lastName, string firstName, string middleName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
        }
    }
}
