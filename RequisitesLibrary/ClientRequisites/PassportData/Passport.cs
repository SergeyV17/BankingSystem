using Microsoft.EntityFrameworkCore;

namespace RequisitesLibrary.ClientRequisites.PassportData
{
    [Owned]
    /// <summary>
    /// Класс паспортных данных
    /// </summary>
    public class Passport
    {
        /// <summary>
        /// Конструктор по умолчанию для EF
        /// </summary>
        public Passport() { }

        /// <summary>
        /// Конструктор паспортных данных
        /// </summary>
        /// <param name="fullName">ФИО</param>
        /// <param name="seriesAndNumber">серия и номер</param>
        /// <param name="address">адрес</param>
        public Passport(FullName fullName, SeriesAndNumber seriesAndNumber, string address)
        {
            FullName = fullName;
            SeriesAndNumber = seriesAndNumber;
            Address = address;
        }

        public FullName FullName { get; private set; }
        public string Address { get; private set; }
        public SeriesAndNumber SeriesAndNumber { get; private set; }
    }
}