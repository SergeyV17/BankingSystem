using System.Windows.Input;
using BankingSystem.Commands;

namespace BankingSystem.ViewModels.EditPanelViewModels
{
    /// <summary>
    /// Класс модели представления для окна "Добавить физ.лицо"
    /// </summary>
    class AddIndividualViewModel : ViewModelBase
    {
        private ICommand addIndividual;
        public ICommand AddIndividual
        {
            get
            {
                return addIndividual ??
                    (addIndividual = new RelayCommand(obj =>
                        {
                            // add client to db
                        }));
            }
        }
    }
}
