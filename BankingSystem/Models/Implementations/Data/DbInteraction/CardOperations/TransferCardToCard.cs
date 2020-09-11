using BankingSystem.Models.Implementations.BankServices.CardService;
using System;
using System.Linq;

namespace BankingSystem.Models.Implementations.Data.DbInteraction.CardOperations
{
    /// <summary>
    /// Класс перевода с карты на карту
    /// </summary>
    class TransferCardToCard
    {
        /// <summary>
        /// Метод перевода с карты на карту
        /// </summary>
        /// <param name="fromCard">карта отправитель</param>
        /// <param name="toCard">карта получатель</param>
        /// <param name="amount">сумма</param>
        /// <returns>признак успешного перевода, сообщение</returns>
        public static (bool successfully, string message) Transfer(Card fromCard, Card toCard, decimal amount)
        {
            using (AppDbContext context = new AppDbContext())
            {
                try
                {
                    var from = context.Cards.FirstOrDefault(c => c.Id == fromCard.Id);
                    var to = context.Cards.FirstOrDefault(c => c.Id == toCard.Id);

                    from.CardBalance -= amount;
                    to.CardBalance += amount;

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }

                return (true,
                    $"Карта {fromCard.CardName} {fromCard.CardNumber}\n\n" +
                    $"Перевод на сумму: {amount:C2}\n" +
                    $"Текущий баланс: {(fromCard.CardBalance - amount):C2}");
            }
        }
    }
}
