namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData.Factories
{
    class ContactFactory
    {
        public static Contact CreateContact(PhoneNumber phoneNumber, string email)
        {
            return new Contact(phoneNumber, email);
        }
    }
}
