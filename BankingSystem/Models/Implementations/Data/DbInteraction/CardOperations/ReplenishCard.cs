using BankingSystem.Models.Implementations.Clients;
using System;
using System.Linq;

namespace BankingSystem.Models.Implementations.Data.DbInteraction.CardOperations
{
    /// <summary>
    /// Класс пополнения лиц. счета карты
    /// </summary>
    class ReplenishCard
    {
        /// <summary>
        /// Метод пополнения карты
        /// </summary>
        /// <param name="client">выбранный аккаунт</param>
        /// <param name="amount"></param>
        /// <returns>признак успешного пополнения, сообщение</returns>
        public static (bool successfully, string message) Replenish(Client client, decimal amount)
        {
            using (AppDbContext context = new AppDbContext())
            {
                try
                {
                    var account = context.Accounts.FirstOrDefault(a => a.Id == client.Account.Id);
                    var card = context.Cards.FirstOrDefault(c => c.Id == client.Account.Card.Id);

                    if (client.Account.AmountOfReplenishmentPerDay + amount > client.Account.ReplenishementPerDayLimit)
                    {
                        account.AccountLockout = true;

                        context.SaveChanges();

                        return (false,
                            "Система зафиксировала превышение максимальной суммы пополнения лицевого счёта.\n" +
                            "Информация\n" +
                            $"Карта: {client.Account.Card.CardName} {client.Account.Card.CardNumber}\n" +
                            $"Владелец: {client.Passport.FullName.Name}\n" +
                            "Решение: Блокировка");
                    }
                    else
                    {
                        account.AmountOfReplenishmentPerDay += amount;
                        card.CardBalance += amount;
                    }

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }

                return (true, 
                    $"Карта {client.Account.Card.CardName} {client.Account.Card.CardNumber}\n\n" + 
                    $"Пополнение на сумму: {amount:C2}\n" +
                    $"Текущий баланс: {(client.Account.Card.CardBalance + amount):C2}");
            }
        }
    }
}
