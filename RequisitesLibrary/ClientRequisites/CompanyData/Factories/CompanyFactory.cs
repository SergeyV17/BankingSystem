namespace RequisitesLibrary.ClientRequisites.CompanyData.Factories
{
    /// <summary>
    /// Класс фабрики компании
    /// </summary>
    public class CompanyFactory
    {
        /// <summary>
        /// Метод создания компании
        /// </summary>
        /// <param name="name">наименование</param>
        /// <param name="website">вебсайт</param>
        public static Company CreateCompany(string name, string website) => new Company(name, website);
    }
}
