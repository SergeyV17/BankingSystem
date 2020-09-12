using BankingSystem.Models.Implementations.BankServices.DepositService;
using BankingSystem.Models.Implementations.Clients;
using BankingSystem.Models.Implementations.Data.DbInteraction.DepositOperations.EventArgs;
using System;
using System.Linq;

namespace BankingSystem.Models.Implementations.Data.DbInteraction.DepositOperations
{
    /// <summary>
    /// Класс закрытия депозита
    /// </summary>
    class СloseDeposit
    {
        public static event EventHandler<CloseDepositEventArgs> DepositClosed;

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

                string message =
                    $"Клиент: {selectedClient.Passport.FullName.Name}\n" +
                    $"Операция: Досрочное закрытие вклада\n" +
                    $"Сумма вклада: {selectedClient.Account.Deposit.DepositBalance:C}\n" +
                    $"Капитализация: {(selectedClient.Account.Deposit.DepositCapitalization ? "Подключена" : "Отключена")}\n" +
                    $"Дата: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                    $"Отчет: Успешно";

                DepositClosed?.Invoke(null, new CloseDepositEventArgs { LogMessage = message });

                return (true, message);
            }
        }
    }
}
