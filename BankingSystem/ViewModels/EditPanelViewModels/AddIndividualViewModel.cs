using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BankingSystem.Commands;

namespace BankingSystem.ViewModels.EditPanelViewModels
{
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
