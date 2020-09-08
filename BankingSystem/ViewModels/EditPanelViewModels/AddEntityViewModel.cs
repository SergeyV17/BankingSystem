using BankingSystem.Commands;
using BankingSystem.Models.Implementations.Accounts;
using System.Windows.Input;

namespace BankingSystem.ViewModels.EditPanelViewModels
{
    /// <summary>
    /// Класс модели представления для окна "Добавить юр.лицо"
    /// </summary>
    class AddEntityViewModel : ViewModelBase
    {
        public AddEntityViewModel(AccountType type)
        {
            Type = type;
        }

        public AccountType Type { get; }

        private ICommand addEntity;
        public ICommand AddEntity
        {
            get
            {
                return addEntity ??
                    (addEntity = new RelayCommand(obj =>
                    {
                        // add client to db
                    }));
            }
        }
    }
}
