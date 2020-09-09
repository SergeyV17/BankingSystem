using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using BankingSystem.Commands;
using BankingSystem.Models.Abstractions;
using BankingSystem.Models.Implementations.Accounts;
using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.Data.DbInteraction;
using BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories;

namespace BankingSystem.ViewModels.EditPanelViewModels
{
    /// <summary>
    /// Класс модели представления для окна "Добавить физ.лицо"
    /// </summary>
    class AddIndividualViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly Dictionary<string, string> errors;

        private Window addIndividualWindow;
        private readonly IMessageService messageService;

        private string lastName;
        private string firstName;
        private string middleName;
        private string series;
        private string number;
        private string email;
        private string phoneNumber;

        /// <summary>
        /// Конструктор модели представления
        /// </summary>
        /// <param name="type">тип аккаунта</param>
        public AddIndividualViewModel(Window addIndividualWindow, IMessageService messageService, AccountType type)
        {
            this.addIndividualWindow = addIndividualWindow;
            this.messageService = messageService;

            Type = type;

            errors = new Dictionary<string, string>
            { 
                [nameof(LastName)] = null,
                [nameof(FirstName)] = null,
                [nameof(MiddleName)] = null,
                [nameof(Series)] = null,
                [nameof(Number)] = null,
                [nameof(PhoneNumber)] = null,
                [nameof(Email)] = null
            };

            CardName = CardNameFactory.CardNamesDictionary[CardNames.VisaClassic];
        }

        public string Error => throw new NotImplementedException();
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;

        public AccountType Type { get; }

        public string LastName 
        { 
            get => lastName; 
            set 
            { 
                lastName = value;

                if (!lastName.All(Char.IsLetter))
                    errors[nameof(LastName)] = "Недопустимые символы.";
                else
                    errors[nameof(LastName)] = null;
            } 
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;

                if (!firstName.All(Char.IsLetter))
                    errors[nameof(FirstName)] = "Недопустимые символы.";
                else
                    errors[nameof(FirstName)] = null;
            }
        }

        public string MiddleName
        {
            get => middleName;
            set
            {
                middleName = value;

                if (!middleName.All(Char.IsLetter))
                    errors[nameof(MiddleName)] = "Недопустимые символы.";
                else
                    errors[nameof(MiddleName)] = null;
            }
        }

        public string Series
        {
            get => series;
            set
            {
                series = value;

                if (!series.All(Char.IsDigit) || series.Length < 4)
                    errors[nameof(Series)] = "Ошибка.";
                else
                    errors[nameof(Series)] = null;
            }
        }

        public string Number
        {
            get => number;
            set
            {
                number = value;

                if (!number.All(Char.IsDigit) || number.Length < 6)
                    errors[nameof(Number)] = "Ошибка.";
                else
                    errors[nameof(Number)] = null;
            }
        }

        public string Address { get; set; }
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;

                if (!phoneNumber.All(Char.IsDigit) || phoneNumber.Length < 10)
                    errors[nameof(PhoneNumber)] = "Ошибка.";
                else
                    errors[nameof(PhoneNumber)] = null;
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;

                var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

                if (!regex.IsMatch(email))
                    errors[nameof(Email)] = "Недопустимые символы.";
                else
                    errors[nameof(Email)] = null;
            }
        }

        public string CardName { get; set; }

        public bool IsValid => errors.Values.All(x => x == null) && lastName != null && firstName != null && middleName != null && series != null && number != null && Address != null && PhoneNumber != null;

        /// <summary>
        /// Команда добавления физ. лица в БД
        /// </summary>
        private ICommand addIndividualCommand;
        public ICommand AddIndividualCommand
        {
            get
            {
                return addIndividualCommand ??
                    (addIndividualCommand = new RelayCommand(obj =>
                        {
                            try
                            {
                                var (successfully, message) = AddClient.AddIndividualToDb(
                                                LastName, FirstName, MiddleName,
                                                Series, Number, Address,
                                                PhoneNumber, Email,
                                                CardName, Type);

                                if (successfully)
                                {
                                    messageService.ShowInfoMessage(addIndividualWindow, message);
                                    addIndividualWindow.Close();
                                }
                                else
                                {
                                    messageService.ShowWarningtMessage(addIndividualWindow, message);
                                }
                            }
                            catch (Exception ex)
                            {
                                messageService.ShowErrorMessage(addIndividualWindow, ex.Message);
                            }
                        },
                        (obj) => IsValid));
            }
        }
    }
}
