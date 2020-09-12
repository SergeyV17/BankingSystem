using BankingSystem.Commands;
using BankingSystem.Models.Abstractions;
using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.Clients;
using BankingSystem.Models.Implementations.Data.DbInteraction.CardOperations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BankingSystem.ViewModels.OperationViewModels
{
    /// <summary>
    /// Класс модели представления для окна "Пополнить карту"
    /// </summary>
    class ReplenishCardViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly Dictionary<string, string> errors;

        private Window replenishCardWindow;
        private readonly IMessageService messageService;

        private string amount;

        private bool reportVisibility;

        /// <summary>
        /// Конструктор модели представления
        /// </summary>
        /// <param name="replenishCardWindow">окно пополнение лиц. счета карты</param>
        /// <param name="messageService">сервис работы с сообщениями</param>
        /// <param name="selectedClient">выбранный клиент</param>
        public ReplenishCardViewModel(Window replenishCardWindow, IMessageService messageService, Client selectedClient)
        {
            this.replenishCardWindow = replenishCardWindow;
            this.messageService = messageService;

            SelectedClient = selectedClient;

            errors = new Dictionary<string, string>
            {
                [nameof(Amount)] = null
            };

            CardName = selectedClient.Account.Card.CardName;
            CardNumber = selectedClient.Account.Card.CardNumber;
            CardBalance = selectedClient.Account.Card.CardBalance;

            CheckFields();
        }

        public string Error => throw new NotImplementedException();
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public Client SelectedClient { get; set; }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public decimal CardBalance { get; set; }

        public string Amount
        {
            get => amount;
            set
            {
                amount = value;

                if (amount == default)
                    errors[nameof(Amount)] = "*";
                else if (!decimal.TryParse(amount,out _))
                    errors[nameof(Amount)] = "Недопустимые символы.";
                else if (decimal.Parse(amount) == default)
                    errors[nameof(Amount)] = "Недопустимое значение.";
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

                CardBalance += decimal.Parse(Amount);
                OnPropertyChanged(nameof(CardBalance));
            }
        }

        /// <summary>
        /// Команда добавления юр. лица в БД
        /// </summary>
        private ICommand replenishCardCommand;
        public ICommand ReplenishCardCommand
        {
            get
            {
                return replenishCardCommand ??
                    (replenishCardCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            var (successfully, message) = ReplenishCard.Replenish(SelectedClient, decimal.Parse(Amount));

                            if (successfully)
                            {
                                ReportVisibility = true;
                                messageService.ShowInfoMessage(replenishCardWindow, message);
                                replenishCardWindow.Close();
                            }
                            else
                            {
                                messageService.ShowWarningMessage(replenishCardWindow, message);
                                replenishCardWindow.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            messageService.ShowErrorMessage(replenishCardWindow, ex.Message);
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
