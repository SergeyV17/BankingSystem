using System;
using System.Windows.Input;
using BankingSystem.Commands;
using BankingSystem.Models.Abstractions;
using BankingSystem.Views;
using BankingSystem.Views.Windows.EditClientPanel;
using BankingSystem.ViewModels.EditPanelViewModels;
using BankingSystem.ViewModels.OperationViewModels;
using BankingSystem.ViewModels.HistoryViewModels;

namespace BankingSystem.ViewModels
{
    /// <summary>
    /// Модель представление главного окна
    /// </summary>
    class MainViewModel : ViewModelBase
    {
        #region Поля

        private readonly MainWindow _mainWindow;

        private readonly IFilePathService _filePathService;
        private readonly IMessageService _messageService;

        #endregion

        #region Свойства

        public AddIndividualViewModel AddIndividuaViewModel { get; private set; }
        public AddEntityViewModel AddEntityViewModel { get; private set; }
        public EditIndividualViewModel EditIndividualViewModel { get; private set; }
        public EditEntityViewModel EditEntityViewModel { get; private set; }

        public ReplenishmentCardViewModel ReplenishmentCardViewModel { get; private set; }
        public TransferViewModel TransferViewModel { get; private set; }
        public OpenDeposiViewModel OpenDeposiViewModel { get; private set; }

        public ClientHistoryViewModel ClientHistoryViewModel { get; private set; }
        public OperationHistoryViewModel OperationHistoryViewModel { get; private set; }

        #endregion


        #region Конструктор

        /// <summary>
        /// Конструктор модели представления главного окна
        /// </summary>
        /// <param name="dialogService"><сервис диалоговых окон/param>
        public MainViewModel(MainWindow mainWindow, IFilePathService filePathService, IMessageService messageService)
        {
            _mainWindow = mainWindow;

            _filePathService = filePathService;
            _messageService = messageService;
        }

        #endregion

        #region Команды

        #region Команды меню

        /// <summary>
        /// Команда выхода из приложения
        /// </summary>
        private ICommand exitCommand;
        public ICommand ExitCommand
        {
            get { return exitCommand ?? (exitCommand = new RelayCommand(obj => { _mainWindow.Close(); })); }
        }

        /// <summary>
        /// Команда открытия файла информации
        /// </summary>
        private ICommand showInfoCommand;
        public ICommand ShowInfoCommand
        {
            get
            {
                return showInfoCommand ??
                (showInfoCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        _filePathService.GetPath(@"Views\Resources\Text\InformationSheet.txt");
                        _messageService.ShowMessageFromFile(_mainWindow, _filePathService.FilePath);
                    }
                    catch (Exception ex)
                    {
                        _messageService.ShowErrorMessage(_mainWindow, ex.Message);
                    }
                }));
            }
        }

        #endregion

        #region Команды панели редактирования клиентов

        private ICommand addClientCommand;
        public ICommand AddClientCommand
        {
            get
            {
                return addClientCommand ??
                    (addClientCommand = new RelayCommand(obj =>
                    {
                        var addIndividualWindow = new AddIndividualWindow() { Owner = _mainWindow, DataContext = AddIndividuaViewModel };

                        addIndividualWindow.ShowDialog();
                    }));
            }
        }

        private ICommand editClientCommand;
        public ICommand EditClientCommand
        {
            get
            {
                return editClientCommand ??
                    (editClientCommand = new RelayCommand(obj =>
                    {
                        var addEntityWindow = new AddEntityWindow() { Owner = _mainWindow, DataContext = EditEntityViewModel };

                        addEntityWindow.ShowDialog();
                    }));
            }
        }

        private ICommand deleteClientCommand;
        public ICommand DeleteClientCommand
        {
            get
            {
                return deleteClientCommand ??
                    (deleteClientCommand = new RelayCommand(obj =>
                    {
                    }));
            }
        }

        #endregion

        #region Команды панели информации о клиенте

        private ICommand showCardCommand;
        public ICommand ShowCardCommand
        {
            get
            {
                return showCardCommand ??
                    (showCardCommand = new RelayCommand(obj =>
                    {
                    }));
            }
        }

        private ICommand showDepositCommand;
        public ICommand ShowDepositCommand
        {
            get
            {
                return showDepositCommand ??
                    (showDepositCommand = new RelayCommand(obj =>
                    {
                    }));
            }
        }

        #endregion

        #region Команды в панели операций

        private ICommand replenishCardCommand;
        public ICommand ReplenishCardCommand
        {
            get
            {
                return replenishCardCommand ??
                    (replenishCardCommand = new RelayCommand(obj =>
                    {
                    }));
            }
        }

        private ICommand transferCommand;
        public ICommand TransferCommand
        {
            get
            {
                return transferCommand ??
                    (transferCommand = new RelayCommand(obj =>
                    {
                    }));
            }
        }

        private ICommand openDepositCommand;
        public ICommand OpenDepositCommand
        {
            get
            {
                return openDepositCommand ??
                    (openDepositCommand = new RelayCommand(obj =>
                    {
                    }));
            }
        }

        private ICommand closeDepositCommand;
        public ICommand CloseDepositCommand
        {
            get
            {
                return closeDepositCommand ??
                    (closeDepositCommand = new RelayCommand(obj =>
                    {
                    }));
            }
        }

        #endregion

        #endregion
    }
}
