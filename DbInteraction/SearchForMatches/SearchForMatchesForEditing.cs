using DataLibrary.Clients;
using RequisitesLibrary.ClientRequisites.CompanyData;
using RequisitesLibrary.ClientRequisites.ContactData;
using RequisitesLibrary.ClientRequisites.PassportData;
using System.Linq;

namespace DbInteraction.SearchesForMatches
{
    /// <summary>
    /// Класс поиска совпадений при редактировании клиентов
    /// </summary>
    class SearchForMatchesForEditing
    {
        /// <summary>
        /// Метод проверки на совпадения базовых полей с БД клиентов
        /// </summary>
        /// <param name="current">выбранный клиент</param>
        /// <param name="passport">пасспортные данные</param>
        /// <param name="contact">контакнтные данные</param>
        /// <returns>признак совпадений, сообщение</returns>
        public static (bool noMatches, string message) BaseErrorProcessing(Client current, Passport passport, Contact contact)
        {
            using (AppDbContext context = new AppDbContext())
            {
                if (context.Clients.AsEnumerable().FirstOrDefault(c => c.Passport.SeriesAndNumber.FullSeriesAndNumber != current.Passport.SeriesAndNumber.FullSeriesAndNumber && c.Passport.SeriesAndNumber.FullSeriesAndNumber == passport.SeriesAndNumber.FullSeriesAndNumber) != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте серию и номер паспорта");
                }
                else if (context.Clients.FirstOrDefault(c => c.Passport.Address != current.Passport.Address && c.Passport.Address == passport.Address) != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте адрес");
                }
                else if (context.Clients.FirstOrDefault(c => c.Contact.PhoneNumber.Number != current.Contact.PhoneNumber.Number &&
                c.Contact.PhoneNumber.Number == contact.PhoneNumber.Number) != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте номер телефона");
                }
                else if (context.Clients.FirstOrDefault(c => c.Contact.Email != current.Contact.Email && c.Contact.Email == contact.Email) != null)
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
        /// <param name="current">выбранный клиент</param>
        /// <param name="passport">паспортные данные</param>
        /// <param name="contact">контактные данные</param>
        /// <returns>признак совпадений, сообщение</returns>
        public static (bool noMatches, string message) IndividualErrorProcessing(Individual current, Passport passport, Contact contact)
        {
            using (AppDbContext context = new AppDbContext())
            {
                if (context.Individuals.AsEnumerable().FirstOrDefault(c => c.Passport.FullName.Name != current.Passport.FullName.Name
                && c.Passport.FullName.Name == passport.FullName.Name) != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте ФИО");
                }

                var (noMatches, message) = BaseErrorProcessing(current, passport, contact);

                if (!noMatches)
                {
                    return (false, BaseErrorProcessing(current, passport, contact).message);
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
        /// <param name="current">выбранный клиент</param>
        /// <param name="passport">паспортные данные</param>
        /// <param name="contact">контактные данные</param>
        /// <returns>признак совпадений, сообщение</returns>
        public static (bool noMatches, string message) EntityErrorProcessing(Entity current, Passport passport, Contact contact, Company company)
        {
            using (AppDbContext context = new AppDbContext())
            {
                if (context.Entities.AsEnumerable().FirstOrDefault(c => c.Passport.FullName.Name != current.Passport.FullName.Name && 
                c.Passport.FullName.Name == passport.FullName.Name) != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте ФИО");
                }

                var (isntMached, message) = BaseErrorProcessing(current, passport, contact);

                if (!isntMached)
                {
                    return (false, BaseErrorProcessing(current, passport, contact).message);
                }
                else if (context.Entities.FirstOrDefault(c => c.Company.Name != current.Company.Name && c.Company.Name == company.Name) != null)
                {
                    return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте наименование компании");
                }
                else if (context.Entities.FirstOrDefault(c => c.Company.Website != current.Company.Website && c.Company.Website == company.Website) != null)
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
