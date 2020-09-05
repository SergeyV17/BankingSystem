using BankingSystem.Commands;
using System.Windows.Input;

namespace BankingSystem.ViewModels.EditPanelViewModels
{
    /// <summary>
    /// Класс модели представления для окна "Добавить юр.лицо"
    /// </summary>
    class AddEntityViewModel : ViewModelBase
    {
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
