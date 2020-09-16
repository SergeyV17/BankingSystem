using DataLibrary.Clients;
using DataLibrary.Deposits;
using DbInteraction.DepositOperations.EventArgs;
using System;
using System.Linq;

namespace DbInteraction.DepositOperations
{
    /// <summary>
    /// Класс закрытия депозита
    /// </summary>
    public class СloseDeposit
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
                try
                {
                    var account = context.Accounts.FirstOrDefault(a => a.Id == selectedClient.Account.Id);
                    var card = context.Cards.FirstOrDefault(c => c.Id == selectedClient.Account.Card.Id);

                    card.CardBalance += selectedClient.Account.Deposit.DepositBalance;
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
