using BankingSystem.Models.Implementations.Requisites.ClientRequisites.CompanyData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;
using System.Linq;

namespace BankingSystem.Models.Implementations.Data.DbInteraction.SearchesForMatches
{
    /// <summary>
    /// Класс поиска совпадений при добавлении клиента в БД
    /// </summary>
    class SearchForMatchesForAdding
    {
        /// <summary>
        /// Метод проверки на совпадения базовых полей с БД клиентов
        /// </summary>
        /// <param name="context">контекст БД</param>
        /// <param name="passport">пасспортные данные</param>
        /// <param name="contact">контакнтные данные</param>
        /// <returns>признак совпадений, сообщение</returns>
        public static (bool noMatches, string message) BaseErrorProcessing(Passport passport, Contact contact)
        {
            using (AppDbContext context = new AppDbContext())
            {
                if (context.Clients.FirstOrDefault(c => c.Passport.SeriesAndNumber.Series == passport.SeriesAndNumber.Series && 
                c.Passport.SeriesAndNumber.Number == passport.SeriesAndNumber.Number) != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте серию и номер паспорта");
                }
                else if (context.Clients.FirstOrDefault(c => c.Passport.Address == passport.Address) != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте адрес");
                }
                else if (context.Clients.FirstOrDefault(c => c.Contact.PhoneNumber.Number == contact.PhoneNumber.Number) != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте номер телефона");
                }
                else if (context.Clients.FirstOrDefault(c => c.Contact.Email == contact.Email) != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте адрес электронной почты");
                }
                else
                {
                    return (true, "Совпадений не найдено");
                }
            }
        }

        /// <summary>
        /// Метод проверки на совпадения полей с БД клиентов
        /// </summary>
        /// <param name="context">контекст БД</param>
        /// <param name="passport">паспортные данные</param>
        /// <param name="contact">контактные данные</param>
        /// <returns>признак совпадений, сообщение</returns>
        public static (bool noMatches, string message) IndividualErrorProcessing(Passport passport, Contact contact)
        {
            using (AppDbContext context = new AppDbContext())
            {
                var individual = context.Individuals.AsEnumerable().FirstOrDefault(c => c.Passport.FullName.Name == passport.FullName.Name);

                if (individual != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте ФИО");
                }

                var (noMatches, message) = BaseErrorProcessing(passport, contact);

                if (!noMatches)
                {
                    return (false, BaseErrorProcessing(passport, contact).message);
                }
                else
                {
                    return (true, "Cовпадений не найдено.");
                }
            }
        }

        /// <summary>
        /// Метод проверки на совпадения полей с БД клиентов
        /// </summary>
        /// <param name="context">контекст БД</param>
        /// <param name="passport">паспортные данные</param>
        /// <param name="contact">контактные данные</param>
        /// <returns>признак совпадений, сообщение</returns>
        public static (bool noMatches, string message) EntityErrorProcessing(Passport passport, Contact contact, Company company)
        {
            using (AppDbContext context = new AppDbContext())
            {
                var entity = context.Entities.AsEnumerable().FirstOrDefault(c => c.Passport.FullName.Name == passport.FullName.Name);

                if (entity != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте ФИО");
                }

                var (noMatches, message) = BaseErrorProcessing(passport, contact);

                if (!noMatches)
                {
                    return (false, BaseErrorProcessing(passport, contact).message);
                }
                else if (context.Entities.FirstOrDefault(c => c.Company.Name == company.Name) != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте наименование компании");
                }
                else if (context.Entities.FirstOrDefault(c => c.Company.Website == company.Website) != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте вебсайт");
                }
                else
                {
                    return (true, "Cовпадений не найдено.");
                }
            }
        }
    }
}
