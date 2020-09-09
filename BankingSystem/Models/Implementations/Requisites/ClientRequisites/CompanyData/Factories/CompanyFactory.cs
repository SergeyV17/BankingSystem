namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.CompanyData.Factories
{
    /// <summary>
    /// Класс фабрики компании
    /// </summary>
    class CompanyFactory
    {
        /// <summary>
        /// Метод создания компании
        /// </summary>
        /// <param name="name">наименование</param>
        /// <param name="website">вебсайт</param>
        public static Company CreateCompany(string name, string website) => new Company(name, website);
    }
}
