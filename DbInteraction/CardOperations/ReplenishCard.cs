using DataLibrary.Clients;
using DbInteraction.CardOperations.EventArgs;
using System;
using System.Linq;

namespace DbInteraction.CardOperations
{
    /// <summary>
    /// Класс пополнения лиц. счета карты
    /// </summary>
    public class ReplenishCard
    {
        public static event EventHandler<ReplenishmentEventArgs> CardReplenished;

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
                string message;

                try
                {
                    var account = context.Accounts.FirstOrDefault(a => a.Id == client.Account.Id);
                    var card = context.Cards.FirstOrDefault(c => c.Id == client.Account.Card.Id);

                    if (client.Account.AmountOfReplenishmentPerDay + amount > client.Account.ReplenishementPerDayLimit)
                    {
                        account.AccountLockout = true;
                        context.SaveChanges();

                        message = "Система зафиксировала превышение максимальной суммы пополнения лицевого счёта.\n" +
                                  "Информация\n" +
                                  $"Карта: {client.Account.Card.CardName} {client.Account.Card.CardNumber}\n" +
                                  $"Владелец: {client.Passport.FullName.Name}\n" +
                                  "Решение: Блокировка";

                        CardReplenished?.Invoke(null, new ReplenishmentEventArgs { LogMessage = message });

                        return (false, message);
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

                message = $"Клиент: {client.Passport.FullName.Name}\n" +
                          $"Карта: {client.Account.Card.CardName}\n" +
                          $"Номер: {client.Account.Card.CardNumber}\n" +
                          $"Текущий баланс: {client.Account.Card.CardBalance:C}\n" +
                          $"Пополнение на сумму: {amount:C}\n" +
                          $"Дата: {DateTime.Now:dd/MM/yyyy HH:mm:ss}\n" + "Отчет: Успешно";

                CardReplenished?.Invoke(null, new ReplenishmentEventArgs { LogMessage = message });

                return (true, message);
            }
        }
    }
}
