using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using BankingSystem.Commands;
using BankingSystem.Models.Abstractions;
using DataLibrary.Accounts;
using DbInteraction.ClientBaseEditing;
using RequisitesLibrary.CardRequisites;
using RequisitesLibrary.CardRequisites.Factories;

namespace BankingSystem.ViewModels.EditPanelViewModels
{
    /// <summary>
    /// Класс модели представления для окна "Добавить физ.лицо"
    /// </summary>
    class AddIndividualViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly Dictionary<string, string> errors;

        private readonly Window addIndividualWindow;
        private readonly IMessageService messageService;

        private string lastName;
        private string firstName;
        private string middleName;
        private string series;
        private string number;
        private string address;
        private string email;
        private string phoneNumber;

        /// <summary>
        /// Конструктор модели представления
        /// </summary>
        /// <param name="addIndividualWindow">окно добавления физ. лица</param>
        /// <param name="messageService">сервис работы с сообщениями</param>
        /// <param name="isVIPNode">признак вип узла</param>
        public AddIndividualViewModel(Window addIndividualWindow, IMessageService messageService, bool isVIPNode)
        {
            this.addIndividualWindow = addIndividualWindow;
            this.messageService = messageService;

            Type = isVIPNode ? AccountType.VIP : AccountType.Regular;

            errors = new Dictionary<string, string>
            { 
                [nameof(LastName)] = null,
                [nameof(FirstName)] = null,
                [nameof(MiddleName)] = null,
                [nameof(Address)] = null,
                [nameof(Series)] = null,
                [nameof(Number)] = null,
                [nameof(PhoneNumber)] = null,
                [nameof(Email)] = null
            };

            CardName = CardNameFactory.CardNamesDictionary[CardNames.VisaClassic];

            CheckFields();
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
                else if (lastName.Length == 0)
                    errors[nameof(LastName)] = "*";
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
                else if (firstName.Length == 0)
                    errors[nameof(FirstName)] = "*";
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
                else if (middleName.Length == 0)
                    errors[nameof(MiddleName)] = "*";
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

                if (!series.All(Char.IsDigit))
                    errors[nameof(Series)] = "Ошибка.";
                else if (series.Length < 4)
                    errors[nameof(Series)] = "*";
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

                if (!number.All(Char.IsDigit))
                    errors[nameof(Number)] = "Ошибка.";
                else if (number.Length < 6)
                    errors[nameof(Number)] = "*";
                else
                    errors[nameof(Number)] = null;
            }
        }

        public string Address
        {
            get => address;
            set
            {
                address = value;

                if (address.Length == 0)
                    errors[nameof(Address)] = "*";
                else
                    errors[nameof(Address)] = null;
            }
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;

                if (!phoneNumber.All(Char.IsDigit))
                    errors[nameof(PhoneNumber)] = "Ошибка.";
                else if (phoneNumber.Length < 10)
                    errors[nameof(PhoneNumber)] = "*";
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

                if (email.Length == 0)
                    errors[nameof(Email)] = "*";
                else if (!regex.IsMatch(email))
                    errors[nameof(Email)] = "Недопустимый email.";
                else
                    errors[nameof(Email)] = null;
            }
        }

        public string CardName { get; set; }

        public bool IsValid => errors.Values.All(x => x == null) && 
            lastName != null && firstName != null && middleName != null && series != null && number != null && Address != null && 
            PhoneNumber != null && Email != null;

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
                                    messageService.ShowWarningMessage(addIndividualWindow, message);
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

        /// <summary>
        /// Метод для подсвечивания обязательных полей при инициализации окна
        /// </summary>
        private void CheckFields()
        {
            LastName = "";
            FirstName = "";
            MiddleName = "";
            Address = "";
            Series = "";
            Number = "";
            PhoneNumber = "";
            Email = "";
        }
    }
}
