using BankingSystem.Models.Implementations.Clients;
using System;

namespace BankingSystem.Models.Implementations.Data.DbInteraction.ClientBaseEditing
{
    /// <summary>
    /// Класс удаления клиентов из БД
    /// </summary>
    class DeleteClient
    {
        /// <summary>
        /// Метод удаления клиента из БД
        /// </summary>
        /// <param name="selectedClient">выбранный клиент</param>
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

                return (true, $"Клиент: {selectedClient.Passport.FullName.Name} успешно удален.");
            }
        }
    }
}
