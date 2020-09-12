using BankingSystem.Models.Implementations.BankServices.DepositService.Factories;
using BankingSystem.Models.Implementations.Clients;
using System;
using System.Linq;

namespace BankingSystem.Models.Implementations.Data.DbInteraction.DepositOperations
{
    /// <summary>
    /// Класс создания депозита
    /// </summary>
    class OpenDeposit
    {
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
                var newDeposit = new DefaultDepositFactory().CreateDeposit(amount, capitalization, type);

                try
                {
                    var card = context.Cards.FirstOrDefault(c => c.Id == selectedClient.Account.Card.Id);
                    var account = context.Accounts.FirstOrDefault(a=> a.Id == selectedClient.Account.Id);

                    var deposit = context.Deposits.FirstOrDefault(d => d.AccountId == selectedClient.Account.Id);
                    context.Remove(deposit);

                    card.CardBalance -= amount;
                    account.Deposit = newDeposit;

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }

                return (true,
                    $"Открытие депозита {newDeposit.DepositNumber}\n\n" +
                    $"Текущий баланс: {newDeposit.DepositBalance:C2}\n" +
                    $"Капитализация: {(newDeposit.DepositCapitalization ? "Подключена" : "Отключена")}\n" +
                    $"Ставка: {newDeposit.DepositRate:P}\n" +
                    $"Дата открытия: {newDeposit.DateOfDepositOpen}\n" +
                    $"Дата закрытия: {newDeposit.DateOfDepositClose}\n\n" +
                    $"Отчет об операции: успешно\n");
            }
        }
    }
}
