using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.Clients;
using System.Collections.Generic;
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
                IEnumerable<Client> list = from clients in context.Clients 
                                           where clients is Individual && clients.Account is RegularAccount
                                           orderby clients.ClientId 
                                           select clients;

                return new ObservableCollection<Client>(list);
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
                IEnumerable<Client> list = from clients in context.Clients
                                           where clients is Entity && clients.Account is RegularAccount
                                           orderby clients.ClientId
                                           select clients;

                return new ObservableCollection<Client>(list);
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
                IEnumerable<Client> list = from clients in context.Clients
                                           where clients is Individual && clients.Account is VIPAccount
                                           orderby clients.ClientId
                                           select clients;

                return new ObservableCollection<Client>(list);
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
                IEnumerable<Client> list = from clients in context.Clients
                                           where clients is Entity && clients.Account is VIPAccount
                                           orderby clients.ClientId
                                           select clients;

                return new ObservableCollection<Client>(list);
            }
        }
    }
}
