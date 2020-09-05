namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData
{
    /// <summary>
    /// Структура номера телефона
    /// </summary>
    readonly struct PhoneNumber
    {
        /// <summary>
        /// Конструктор номера телефона
        /// </summary>
        /// <param name="number">номер телефона</param>
        public PhoneNumber(string number)
        {
            this.Number = number;
        }

        public string Number { get; }
    }
}
