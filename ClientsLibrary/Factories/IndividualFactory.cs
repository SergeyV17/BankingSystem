using AccountsLibrary;
using RequisitesLibrary.ClientRequisites.ContactData;
using RequisitesLibrary.ClientRequisites.PassportData;

namespace ClientsLibrary.Factories
{
    /// <summary>
    /// Фабрика физ. лиц
    /// </summary>
    class IndividualFactory
    {
        /// <summary>
        /// Метод создания физ. лица
        /// </summary>
        /// <param name="passport">пасспортные данные</param>
        /// <param name="contact">контактные данные</param>
        /// <param name="account">аккаунт</param>
        /// <returns>физ. лицо</returns>
        public static Individual CreateIndividual(Passport passport, Contact contact, Account account) => new Individual(passport, contact, account);
    }
}
