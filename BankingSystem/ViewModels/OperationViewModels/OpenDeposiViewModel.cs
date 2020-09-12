using BankingSystem.Commands;
using BankingSystem.Models.Abstractions;
using BankingSystem.Models.Implementations.BankServices.DepositService;
using BankingSystem.Models.Implementations.Clients;
using BankingSystem.Models.Implementations.Data.DbInteraction.DepositOperations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BankingSystem.ViewModels.OperationViewModels
{
    /// <summary>
    /// Класс модели представления для окна "Открыть депозит"
    /// </summary>
    class OpenDeposiViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly Dictionary<string, string> errors;

        private readonly Window openDepositWindow;
        private readonly IMessageService messageService;

        private string amount;

        private bool reportVisibility;

        /// <summary>
        /// Конструктор модели представления
        /// </summary>
        /// <param name="openDepsitWindow">окно пополнение лиц. счета карты</param>
        /// <param name="messageService">сервис работы с сообщениями</param>
        /// <param name="selectedClient">выбранный клиент</param>
        public OpenDeposiViewModel(Window openDepsitWindow, IMessageService messageService, Client selectedClient)
        {
            this.openDepositWindow = openDepsitWindow;
            this.messageService = messageService;

            SelectedClient = selectedClient;

            errors = new Dictionary<string, string>
            {
                [nameof(Amount)] = null
            };

            CardBalance = selectedClient.Account.Card.CardBalance;
            Capitalization = selectedClient.Account.Deposit.DepositCapitalization;
            DepositRate = DepositRates.GetDepositRate(ClientType);

            CheckFields();
        }

        public string Error => throw new NotImplementedException();
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public Client SelectedClient { get; set; }

        public ClientType ClientType => SelectedClient is Individual ? ClientType.Individual : ClientType.Entity;

        public decimal CardBalance { get; set; }
        public bool Capitalization { get; set; }
        public double DepositRate { get; set; }

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
                else if (CardBalance - decimal.Parse(amount) < 0)
                    errors[nameof(Amount)] = "Недостаточно средств.";
                else if (decimal.Parse(amount) < 20_000)
                    errors[nameof(Amount)] = "Недопустимая сумма.";
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
        private ICommand openDepositCommand;
        public ICommand OpenDepositCommand
        {
            get
            {
                return openDepositCommand ??
                    (openDepositCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            var (successfully, message) = OpenDeposit.Open(decimal.Parse(amount), Capitalization, ClientType, SelectedClient);

                            if (successfully)
                            {
                                ReportVisibility = true;
                                messageService.ShowInfoMessage(openDepositWindow, message);
                                openDepositWindow.Close();
                            }
                            else
                            {
                                messageService.ShowWarningMessage(openDepositWindow, message);
                                openDepositWindow.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            messageService.ShowErrorMessage(openDepositWindow, ex.Message);
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
