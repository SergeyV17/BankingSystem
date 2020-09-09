using BankingSystem.Models.Implementations.Clients;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData;
using BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData;
using System.Linq;

namespace BankingSystem.Models.Implementations.Data.DbInteraction
{
    /// <summary>
    /// Класс поиска совпадений реквизитов клиентов в БД
    /// </summary>
    class SearchForMatches
    {
        /// <summary>
        /// Метод проверки на совпадения полей с БД клиентов
        /// </summary>
        /// <param name="context">контекст БД</param>
        /// <param name="passport">паспортные данные</param>
        /// <param name="contact">контактные данные</param>
        /// <returns>признак совпадений, сообщение</returns>
        public static (bool IsntMached, string message) ErrorProcessing(AppDbContext context, Passport passport, Contact contact)
        {
            var client = context.Clients.AsEnumerable().FirstOrDefault(c => c.Passport.FullName.Name == passport.FullName.Name);

            if (client != null && client is Individual)
            {
                return (false, "Клиент с введёнными реквизитами уже существует\n-проверьте ФИО");
            }
            else if (context.Clients.FirstOrDefault(c => c.Passport.SeriesAndNumber.Series == passport.SeriesAndNumber.Series
            && c.Passport.SeriesAndNumber.Number == passport.SeriesAndNumber.Number) != null)
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
                return (true, $"Клиент {passport.FullName.Name} успешно добавлен.");
            }
        }
    }
}
