namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData
{
    readonly struct Contact
    {
        public Contact(PhoneNumber phoneNumber, string email)
        {
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public PhoneNumber PhoneNumber { get; }
        public string Email { get; }
    }
}