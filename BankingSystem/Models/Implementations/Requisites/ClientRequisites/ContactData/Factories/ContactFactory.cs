﻿namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.ContactData.Factories
{
    /// <summary>
    /// Класс фабрики контактных данных
    /// </summary>
    class ContactFactory
    {
        /// <summary>
        /// Метод создания контактных данных
        /// </summary>
        /// <param name="phoneNumber">номер телефона</param>
        /// <param name="email">электронная почта</param>
        /// <returns>контактные данные</returns>
        public static Contact CreateContact(PhoneNumber phoneNumber, string email) => new Contact(phoneNumber, email);
    }
}
