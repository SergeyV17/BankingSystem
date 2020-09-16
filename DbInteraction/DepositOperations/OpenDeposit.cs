using DataLibrary.Clients;
using DataLibrary.Deposits.Factories;
using DbInteraction.DepositOperations.EventArgs;
using RequisitesLibrary;
using System;
using System.Linq;

namespace DbInteraction.DepositOperations
{
    /// <summary>
    /// Класс создания депозита
    /// </summary>
    public class OpenDeposit
    {
        public static event EventHandler<OpenDepositEventArgs> DepositOpened;

        /// <summary>
        /// Метод создания депозита
        /// </summary>
        /// <param name="amount">сумма</param>
        /// <param name="capitalization">капитализация</param>
        /// <param name="type">тип клиента</param>
        /// <param name="selectedClient">выбранный клиент</param>
        /// <returns>признак успешного создания депозита, сообщение</returns>
        public static (bool successfully, string message) Open(decimal amount, bool capitalization, ClientType type, Client selectedClient)
        {
            using (AppDbContext context = new AppDbContext())
            {
                try
                {
                    var card = context.Cards.FirstOrDefault(c => c.Id == selectedClient.Account.Card.Id);
                    var account = context.Accounts.FirstOrDefault(a => a.Id == selectedClient.Account.Id);

                    card.CardBalance -= amount;
                    account.Deposit = new DefaultDepositFactory().CreateDeposit(amount, capitalization, type);

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }

                string message = 
                    $"Клиент: {selectedClient.Passport.FullName.Name}\n" +
                    $"Операция: Открытие вклада\n" +
                    $"Сумма вклада: {amount:C}\n" +
                    $"Капитализация: {(capitalization ? "Подключена" : "Отключена")}\n" +
                    $"Дата: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" +
                    $"Отчет: Успешно";

                DepositOpened?.Invoke(null, new OpenDepositEventArgs { LogMessage = message });

                return (true, message);
            }
        }
    }
}
