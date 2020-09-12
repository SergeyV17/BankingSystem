using BankingSystem.Models.Implementations.BankServices.DepositService;
using BankingSystem.Models.Implementations.Clients;
using System;
using System.Linq;

namespace BankingSystem.Models.Implementations.Data.DbInteraction.DepositOperations
{
    /// <summary>
    /// Класс закрытия депозита
    /// </summary>
    class СloseDeposit
    {
        /// <summary>
        /// Метод закрытия депозита
        /// </summary>
        /// <param name="selectedClient"></param>
        /// <returns>признак успешной операции, сообщение</returns>
        public static (bool successfully, string message) Close(Client selectedClient)
        {
            using (AppDbContext context = new AppDbContext())
            {
                Deposit deposit = null;

                try
                {
                    var card = context.Cards.FirstOrDefault(c => c.AccountId == selectedClient.Account.Id);
                    card.CardBalance += selectedClient.Account.Deposit.DepositBalance;

                    deposit = context.Deposits.FirstOrDefault(d => d.AccountId == selectedClient.Account.Id);
                    context.Remove(deposit);

                    var account = context.Accounts.FirstOrDefault(a => a.Id == selectedClient.Account.Id);
                    account.Deposit = new NullDeposit();

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }

                return (true,
                    $"Закрытие депозита {deposit.DepositNumber}\n\n" +
                    $"Текущий баланс: {deposit.DepositBalance:C2}\n" +
                    $"Капитализация: {(deposit.DepositCapitalization ? "Подключена" : "Отключена")}\n" +
                    $"Ставка: {deposit.DepositRate:P}\n" +
                    $"Дата открытия: {deposit.DateOfDepositOpen}\n" +
                    $"Дата закрытия: {DateTime.Now}\n\n" +
                    $"Отчет об операции: успешно\n");
            }
        }
    }
}
