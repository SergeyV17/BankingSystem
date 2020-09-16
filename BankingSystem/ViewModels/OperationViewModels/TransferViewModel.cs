using BankingSystem.Commands;
using BankingSystem.Models.Abstractions;
using DataLibrary.Clients;
using DbInteraction.CardOperations;
using DbInteraction.Selections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BankingSystem.ViewModels.OperationViewModels
{
    /// <summary>
    /// Класс модели представления для окна "Перевод"
    /// </summary>
    class TransferViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly Dictionary<string, string> errors;

        private readonly Window transferWindow;
        private readonly IMessageService messageService;

        private string amount;

        private bool reportVisibility;

        /// <summary>
        /// Конструктор модели представления
        /// </summary>
        /// <param name="transferWindow">окно перевода с карты на карту</param>
        /// <param name="messageService">сервис работы с сообщениями</param>
        /// <param name="fromClient">клиент отправитель</param>
        public TransferViewModel(Window transferWindow, IMessageService messageService, Client fromClient)
        {
            this.transferWindow = transferWindow;
            this.messageService = messageService;

            FromClient = fromClient;

            errors = new Dictionary<string, string>
            {
                [nameof(Amount)] = null
            };

            Clients = fromClient is Individual ? SelectClients.SelectAllIndividuals(fromClient) : SelectClients.SelectAllEntities(fromClient);

            CheckFields();
        }

        public string Error => throw new NotImplementedException();
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public Client FromClient { get; set; }

        public IEnumerable<Client> Clients { get; set; }

        public string Amount
        {
            get => amount;
            set
            {
                amount = value;

                if (amount == default)
                    errors[nameof(Amount)] = "*";
                else if (!decimal.TryParse(amount, out _))
                    errors[nameof(Amount)] = "Недопустимые символы.";
                else if (decimal.Parse(amount) == default)
                    errors[nameof(Amount)] = "Недопустимое значение.";
                else if (FromClient.Account.Card.CardBalance - decimal.Parse(amount) < 0)
                    errors[nameof(Amount)] = "Недостаточно средств.";
                else if (decimal.Parse(amount) > 300_000)
                    errors[nameof(Amount)] = "Превышен лимит.";
                else
                    errors[nameof(Amount)] = null;
            }
        }

        public bool IsValid => errors.Values.All(x => x == null) && Amount != default;

        public bool ReportVisibility
        {
            get => reportVisibility;
            set
            {
                reportVisibility = value;
                OnPropertyChanged(nameof(ReportVisibility));
            }
        }

        /// <summary>
        /// Команда добавления юр. лица в БД
        /// </summary>
        private ICommand transferCommand;
        public ICommand TransferCommand
        {
            get
            {
                return transferCommand ??
                    (transferCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            var toClient = obj as Client;

                            var (successfully, message) = TransferCardToCard.Transfer(FromClient, toClient, decimal.Parse(Amount));

                            if (successfully)
                            {
                                ReportVisibility = true;
                                messageService.ShowInfoMessage(transferWindow, message);
                                transferWindow.Close();
                            }
                            else
                            {
                                messageService.ShowWarningMessage(transferWindow, message);
                            }
                        }
                        catch (Exception ex)
                        {
                            messageService.ShowErrorMessage(transferWindow, ex.Message);
                        }
                    },
                    (obj) => IsValid));
            }
        }

        /// <summary>
        /// Метод для подсвечивания обязательных полей при инициализации окна
        /// </summary>
        private void CheckFields()
        {
            Amount = default;
        }
    }
}
