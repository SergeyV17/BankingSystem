using BankingSystem.Models.Implementations.Clients;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BankingSystem.Models.Implementations.Data.DbInteraction
{
    static class SelectClients
    {
        public static ObservableCollection<Client> SelectAllClients()
        {
            using (AppDbContext context = new AppDbContext())
            {
                IEnumerable<Client> list = from clients in context.Clients select clients;

                return new ObservableCollection<Client>(list);
            }
        }
    }
}
