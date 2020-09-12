using BankingSystem.Models.Implementations.Clients;
using BankingSystem.Models.Implementations.Data.DbInteraction.CardOperations.EventArgs;
using System;
using System.Linq;

namespace BankingSystem.Models.Implementations.Data.DbInteraction.CardOperations
{
    /// <summary>
    /// Класс перевода с карты на карту
    /// </summary>
    class TransferCardToCard
    {
        public static event EventHandler<TransferEventArgs> TransferredCardToCard;

        /// <summary>
        /// Метод перевода с карты на карту
        /// </summary>
        /// <param name="fromClient">отправитель</param>
        /// <param name="toClient">получатель</param>
        /// <param name="amount">сумма</param>
        /// <returns>признак успешного перевода, сообщение</returns>
        public static (bool successfully, string message) Transfer(Client fromClient, Client toClient, decimal amount)
        {
            using (AppDbContext context = new AppDbContext())
            {
                try
                {
                    var from = context.Cards.FirstOrDefault(c => c.AccountId == fromClient.Account.Id);
                    var to = context.Cards.FirstOrDefault(c => c.AccountId == toClient.Account.Id);

                    from.CardBalance -= amount;
                    to.CardBalance += amount;

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }

                string message = 
                    $"Отправитель: {fromClient.Passport.FullName.Name}\n" +
                    $"Карта: {fromClient.Account.Card.CardName} {fromClient.Account.Card.CardNumber}\n" +
                    $"Баланс: {fromClient.Account.Card.CardBalance:C}\n" +
                    $"Получатель: {toClient.Passport.FullName.Name}\n" +
                    $"Карта: {toClient.Account.Card.CardName} {toClient.Account.Card.CardNumber}\n" +
                    $"Баланс: {toClient.Account.Card.CardBalance:C}\n" +
                    $"Перевод на сумму: {amount:C}\n" +
                    $"Дата: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" + "Отчет: Успешно";

                TransferredCardToCard?.Invoke(null, new TransferEventArgs { LogMessage = message });

                return (true, message);
            }
        }
    }
}
