﻿using Microsoft.EntityFrameworkCore;

namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.PassportData
{
    [Owned]
    /// <summary>
    /// Класс ФИО
    /// </summary>
    class FullName
    {
        /// <summary>
        /// Конструктор ФИО
        /// </summary>
        /// <param name="lastName">фамилия</param>
        /// <param name="firstName">имя</param>
        /// <param name="middleName">отчество</param>
        public FullName(string lastName, string firstName, string middleName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.MiddleName = middleName;
        }

        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
    }
}
