using System;
using BankingSystem.Models.Implementations.Requisites.DepositRequisites.Factories;

namespace BankingSystem.Models.Implementations.BankServices.DepositService
{
    /// <summary>
    /// Класс депозита
    /// </summary>
    class Deposit : IDeposit
    {
        public string DepositNumber { get; } 
        public decimal DepositBalance { get; }
        public DateTime DateOfDepositOpen { get; }
        public DateTime DateOfDepositClose { get; }
        public bool DepositCapitalization { get; }
        public decimal DepositRate { get; set; }

        /// <summary>
        /// Конструктор депозита
        /// </summary>
        /// <param name="depositBalance">баланс</param>
        /// <param name="depositCapitalization">капитализация</param>
        /// <param name="depositRate">ставка</param>
        public Deposit(decimal depositBalance, bool depositCapitalization, decimal depositRate)
        {
            DepositNumber = DepositNumberFactory.CreateDepositNumber();
            DepositBalance = depositBalance;
            DateOfDepositOpen = DateTime.Now;
            DateOfDepositClose = DateTime.Now.AddYears(1);
            DepositCapitalization = depositCapitalization;
            DepositRate = depositRate;
        }
    }
}
