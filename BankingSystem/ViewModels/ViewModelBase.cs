using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankingSystem.ViewModels
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Метод вызывающий событие PropertyChanged для оповещения UI об изменениях
        /// </summary>
        /// <param name="propertyName">наименование свойства</param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
