using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.CompanyData
{
    [Owned]
    /// <summary>
    /// Структура данных о компании
    /// </summary>
    class Company
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

        public string Name { get; private set; }
        public string Website { get; private set; }
    }
}