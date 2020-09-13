using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Clients;
using BankingSystem.Models.Implementations.Data.DbInteraction.ClientBaseEditing.EventArgs;
using System;
using System.Windows.Forms;

namespace BankingSystem.Models.Implementations.Data.DbInteraction.ClientBaseEditing
{
    /// <summary>
    /// Класс удаления клиентов из БД
    /// </summary>
    class DeleteClient
    {
        public static event EventHandler<DeleteClientEventArgs> ClientDeleted;

        /// <summary>
        /// Метод удаления клиента из БД
        /// </summary>
        /// <param name="selectedClient">выбранный клиент</param>
        /// <returns>признак успешной операции, сообщение</returns>
        public static (bool successfully, string message) DeleteClientFromDb(Client selectedClient)
        {
            using (AppDbContext context = new AppDbContext())
            {
                try
                {
                    context.Clients.Remove(selectedClient);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return (false, ex.Message);
                }

                string message = "Произведена операция удаления:\n" +
                                 $"Клиент: {selectedClient.Passport.FullName.Name}\n" +
                                 $"Карта: {selectedClient.Account.Card.CardName}\n" +
                                 $"Номер: {selectedClient.Account.Card.CardNumber}\n" +
                                 $"Статус: {(selectedClient.Account is RegularAccount ? "Стандарт" : "VIP")}\n" +
                                 $"Дата: {DateTime.Now: dd/MM/yyyy HH:mm:ss}\n" +
                                 "Отчет: Успешно";

                ClientDeleted?.Invoke(null, new DeleteClientEventArgs { LogMessage = message });

                return (true, message);
            }
        }
    }
}
