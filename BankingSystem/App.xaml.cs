using BankingSystem.Models.Implementations.Data.DbInteraction.CardOperations;
using BankingSystem.Models.Implementations.Data.DbInteraction.ClientBaseEditing;
using BankingSystem.Models.Implementations.Data.DbInteraction.DepositOperations;
using BankingSystem.ViewModels.HistoryViewModels;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace BankingSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() { }

        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU"); ;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU"); ;

            FrameworkElement.LanguageProperty.OverrideMetadata(
              typeof(FrameworkElement),
              new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            SubscribeToLogs();

            base.OnStartup(e);
        }

        /// <summary>
        /// Метод подписки на события для записи в логи
        /// </summary>
        private void SubscribeToLogs()
        {
            //События редактирования БД клиентов
            AddClient.ClientAdded += ClientHistoryViewModel.OnClientAdded;
            EditClient.ClientEdited += ClientHistoryViewModel.OnClientEdited;
            DeleteClient.ClientDeleted += ClientHistoryViewModel.OnClientDeleted;

            // События операций
            ReplenishCard.CardReplenished += OperationHistoryViewModel.OnCardReplenished;
            TransferCardToCard.TransferredCardToCard += OperationHistoryViewModel.OnTransferCardToCard;
            OpenDeposit.DepositOpened += OperationHistoryViewModel.OnDepositOpen;
            СloseDeposit.DepositClosed += OperationHistoryViewModel.OnDepositClose;              
        }
    }
}
