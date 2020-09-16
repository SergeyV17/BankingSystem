using DbInteraction.CardOperations.EventArgs;
using DbInteraction.DepositOperations.EventArgs;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BankingSystem.ViewModels.HistoryViewModels
{
    /// <summary>
    /// Класс модели представления для окна "История операций"
    /// </summary>
    class OperationHistoryViewModel : ViewModelBase
    {
        public static IList<string> ReplenishementList { get; private set; }
        public static IList<string> TransactionList { get; private set; }
        public static IList<string> DepositList { get; private set; }

        /// <summary>
        /// Конструктор модели представления для окна "История операций"
        /// </summary>
        static OperationHistoryViewModel()
        {
            ReplenishementList = new ObservableCollection<string>();
            TransactionList = new ObservableCollection<string>();
            DepositList = new ObservableCollection<string>();
        }

        /// <summary>
        /// Метод загрузки отчета о пополнении карты в логлист
        /// </summary>
        /// <param name="source">источник</param>
        /// <param name="args">аргументы</param>
        public static void OnCardReplenished(object source, ReplenishmentEventArgs args) => ReplenishementList.Add(args.LogMessage);

        /// <summary>
        /// Метод загрузки отчета о трансфере с карты на карту в логлист
        /// </summary>
        /// <param name="source">источник</param>
        /// <param name="args">аргументы</param>
        public static void OnTransferCardToCard(object source, TransferEventArgs args) => TransactionList.Add(args.LogMessage);

        /// <summary>
        /// Метод загрузки отчета о открытии депозита в логлист
        /// </summary>
        /// <param name="source">источник</param>
        /// <param name="args">аргументы</param>
        public static void OnDepositOpen(object source, OpenDepositEventArgs args) => DepositList.Add(args.LogMessage);

        /// <summary>
        /// Метод загрузки отчета о закрытии депозита в логлист
        /// </summary>
        /// <param name="source">источник</param>
        /// <param name="args">аргументы</param>
        public static void OnDepositClose(object source, CloseDepositEventArgs args) => DepositList.Add(args.LogMessage);
    }
}
