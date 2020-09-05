namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData
{
    /// <summary>
    /// Структура контактных данных
    /// </summary>
    readonly struct Contact
    {
        /// <summary>
        /// Конструктор контактных данных
        /// </summary>
        /// <param name="phoneNumber">номер телефона</param>
        /// <param name="email">электронная почта</param>
        public Contact(PhoneNumber phoneNumber, string email)
        {
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public PhoneNumber PhoneNumber { get; }
        public string Email { get; }
    }
}