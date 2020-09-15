using System;
using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Clients;
using BankingSystem.Models.Implementations.Requisites.DepositRequisites.Factories;

namespace BankingSystem.Models.Implementations.BankServices.DepositService
{
    /// <summary>
    /// Класс депозита
    /// </summary>
    abstract class Deposit
    {
        private string depositNumber;

        /// <summary>
        /// Конструктор по умолчанию для EF
        /// </summary>
        public Deposit() { }

        /// <summary>
        /// Конструктор депозита
        /// </summary>
        /// <param name="depositBalance">баланс</param>
        /// <param name="depositCapitalization">капитализация</param>
        /// <param name="depositRate">ставка</param>
        public Deposit(decimal depositBalance, bool depositCapitalization, double depositRate)
        {
            DepositNumber = $"№{DepositNumberFactory.CreateDepositNumber()}";
            DepositBalance = depositBalance;
            DateOfDepositOpen = DateTime.Now;
            DateOfDepositClose = DateTime.Now.AddYears(1);
            DepositCapitalization = depositCapitalization;
            DepositRate = depositRate;
        }

        public int Id { get; set; }
        public bool HasDeposit => !IsNull();
        public string DepositNumber { get => HasDeposit ? depositNumber : "Отсутствует"; private set { depositNumber = value; } }
        public decimal DepositBalance { get; private set; }
        public DateTime DateOfDepositOpen { get; private set; }
        public DateTime DateOfDepositClose { get; private set; }
        public bool DepositCapitalization { get; private set; }
        public double DepositRate { get; set; }

        //Свойства для БД
        //public int AccountId { get; set; }
        public Account Account { get; set; }

        /// <summary>
        /// Метод возвращаюший состояние объекта
        /// </summary>
        /// <returns>булевая переменная</returns>
        public abstract bool IsNull();
    }
}
