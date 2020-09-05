using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData
{
    [Owned]
    /// <summary>
    /// Класс номера телефона
    /// </summary>
    class PhoneNumber
    {
        /// <summary>
        /// Конструктор номера телефона
        /// </summary>
        /// <param name="number">номер телефона</param>
        public PhoneNumber(string number)
        {
            this.Number = number;
        }

        public string Number { get; private set; }
    }
}
