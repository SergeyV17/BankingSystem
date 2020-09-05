using System;

namespace BankingSystem.Models.Implementations.BankServices.DepositService
{
    /// <summary>
    /// Интерфейс определяющий депозит
    /// </summary>
    interface IDeposit
    {
        string DepositNumber { get; }
        decimal DepositBalance { get; }
        DateTime DateOfDepositOpen { get; }
        DateTime DateOfDepositClose { get; }
        bool DepositCapitalization { get; }
        decimal DepositRate { get; set; }
    }
}
