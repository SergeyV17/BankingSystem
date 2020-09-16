using Microsoft.EntityFrameworkCore;

namespace RequisitesLibrary.ClientRequisites.ContactData
{
    [Owned]
    /// <summary>
    /// Класс номера телефона
    /// </summary>
    public class PhoneNumber
    {
        /// <summary>   
        /// Конструктор номера телефона
        /// </summary>
        /// <param name="number">номер телефона</param>
        public PhoneNumber(string number)
        {
            Number = number;
        }

        public string Number { get; private set; }
    }
}
