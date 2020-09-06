using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Clients;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;

namespace BankingSystem.Models.Implementations.Data.DbInteraction
{
    /// <summary>
    /// Класс выборки клиентов из БД
    /// </summary>
    static class SelectClients
    {
        /// <summary>
        /// Метод выборки физ.лиц
        /// </summary>
        /// <returns>список физ. лиц</returns>
        public static ObservableCollection<Client> SelectIndividuals()
        {
            using (AppDbContext context = new AppDbContext())
            {
                var clients = context.Clients.
                    Where(c => c is Individual && c.Account is RegularAccount).
                    OrderBy(c => c.Id)
                    .Include(client => client.Account).
                        ThenInclude(account => account.Card).
                    Include(client => client.Account).
                        ThenInclude(account => account.Deposit);

                return new ObservableCollection<Client>(clients);
            }
        }

        /// <summary>
        /// Метод выборки юр. лиц
        /// </summary>
        /// <returns>список юр. лиц</returns>
        public static ObservableCollection<Client> SelectEntities()
        {
            using (AppDbContext context = new AppDbContext())
            {
                var clients = context.Clients.
                    Where(c => c is Entity && c.Account is RegularAccount).
                    OrderBy(c => c.Id)
                    .Include(client => client.Account).
                        ThenInclude(account => account.Card).
                    Include(client => client.Account).
                        ThenInclude(account => account.Deposit);

                return new ObservableCollection<Client>(clients);
            }
        }

        /// <summary>
        /// Метод выборки ВИП физ. лиц
        /// </summary>
        /// <returns>список ВИП физ. лиц</returns>
        public static ObservableCollection<Client> SelectVIPIndividuals()
        {
            using (AppDbContext context = new AppDbContext())
            {
                var clients = context.Clients.
                    Where(c => c is Individual && c.Account is VIPAccount).
                    OrderBy(c => c.Id)
                    .Include(client => client.Account).
                        ThenInclude(account => account.Card).
                    Include(client => client.Account).
                        ThenInclude(account => account.Deposit);

                return new ObservableCollection<Client>(clients);
            }
        }

        /// <summary>
        /// Метод выборки ВИП юр. лиц
        /// </summary>
        /// <returns>список ВИП юр. лиц</returns>
        public static ObservableCollection<Client> SelectVIPEntities()
        {
            using (AppDbContext context = new AppDbContext())
            {
                var clients = context.Clients.
                    Where(c => c is Entity && c.Account is VIPAccount).
                    OrderBy(c => c.Id)
                    .Include(client => client.Account).
                        ThenInclude(account => account.Card).
                    Include(client => client.Account).
                        ThenInclude(account => account.Deposit);

                return new ObservableCollection<Client>(clients);
            }
        }
    }
}
