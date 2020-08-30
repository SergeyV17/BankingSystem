namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites
{
    readonly struct PhoneNumber
    {
        public string Number { get; }

        public PhoneNumber(string number)
        {
            this.Number = number;
        }
    }
}
