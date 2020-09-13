using BankingSystem.Models.Implementations.Data.DbInteraction.ClientBaseEditing.EventArgs;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BankingSystem.ViewModels.HistoryViewModels
{
    /// <summary>
    /// Класс модели представления для окна "История редактирования клиентов"
    /// </summary>
    class ClientHistoryViewModel : ViewModelBase
    {
        public static IList<string> AddClientList { get; private set; }
        public static IList<string> EditClientList { get; private set; }
        public static IList<string> DeleteClientList { get; private set; }

        static ClientHistoryViewModel()
        {
            AddClientList = new ObservableCollection<string>();
            EditClientList = new ObservableCollection<string>();
            DeleteClientList = new ObservableCollection<string>();
        }

        /// <summary>
        /// Метод загрузки отчета о добавлении клиента в лог лист
        /// </summary>
        /// <param name="source">источник</param>
        /// <param name="args">аргументы</param>
        public static void OnClientAdded(object source, AddClientEventArgs args) => AddClientList.Add(args.LogMessage);

        /// <summary>
        /// Метод загрузки отчета о редактировании клиента в лог лист
        /// </summary>
        /// <param name="source">источник</param>
        /// <param name="args">аргументы</param>
        public static void OnClientEdited(object source, EditClientEventArgs args) =>  EditClientList.Add(args.LogMessage);

        /// <summary>
        /// Метод загрузки отчета о удалении клиента в лог лист
        /// </summary>
        /// <param name="source">источник</param>
        /// <param name="args">аргументы</param>
        public static void OnClientDeleted(object source, DeleteClientEventArgs args) => DeleteClientList.Add(args.LogMessage);
    }
}
