namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.CompanyData
{
    /// <summary>
    /// Структура данных о компании
    /// </summary>
    readonly struct Company
    {
        /// <summary>
        /// Конструктор данных о компании
        /// </summary>
        /// <param name="name">наименование</param>
        /// <param name="website">сайт</param>
        public Company(string name, string website)
        {
            Name = name;
            Website = website;
        }

        public string Name { get; }
        public string Website { get;  }
    }
}