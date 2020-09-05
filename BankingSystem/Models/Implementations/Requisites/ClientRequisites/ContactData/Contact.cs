using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData
{
    [Owned]
    /// <summary>
    /// Класс контактных данных
    /// </summary>
    class Contact
    {
        /// <summary>
        /// Конструктор по умолчанию для EF
        /// </summary>
        public Contact() { }

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

        public PhoneNumber PhoneNumber { get; private set; }
        public string Email { get; private set; }
    }
}