namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData
{
    /// <summary>
    /// Структура ФИО
    /// </summary>
    readonly struct FullName
    {
        /// <summary>
        /// Конструктор ФИО
        /// </summary>
        /// <param name="lastName">фамилия</param>
        /// <param name="firstName">имя</param>
        /// <param name="middleName">отчество</param>
        public FullName(string lastName, string firstName, string middleName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
        }

        public string LastName { get; }
        public string FirstName { get; }
        public string MiddleName { get; }
    }
}
