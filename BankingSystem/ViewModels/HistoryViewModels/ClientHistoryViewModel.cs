using System.Collections.ObjectModel;

namespace BankingSystem.ViewModels.HistoryViewModels
{
    /// <summary>
    /// Класс модели представления для окна "История редактирования клиентов"
    /// </summary>
    class ClientHistoryViewModel : ViewModelBase
    {
        public static ObservableCollection<string> addClientList;
        public static ObservableCollection<string> editClientList;
        public static ObservableCollection<string> deleteClientList;

        static ClientHistoryViewModel()
        {
            addClientList = new ObservableCollection<string>();
            editClientList = new ObservableCollection<string>();
            deleteClientList = new ObservableCollection<string>();
        }

        public static void AddAddOperationClientInfo(string Information)
        {
            addClientList.Add(Information);
        }

        public static void AddEditOperationClientInfo(string Information)
        {
            editClientList.Add(Information);
        }

        public static void AddDeleteOperationClientInfo(string Information)
        {
            deleteClientList.Add(Information);
        }
    }
}
