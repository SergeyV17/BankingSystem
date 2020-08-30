using BankingSystem.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankingSystem.ViewModels.EditPanelViewModels
{
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
