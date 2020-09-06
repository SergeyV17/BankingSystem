using System;
using System.Windows.Input;
using BankingSystem.Commands;
using BankingSystem.Models.Abstractions;
using BankingSystem.Views;
using BankingSystem.Views.Windows.EditClientPanel;
using BankingSystem.ViewModels.EditPanelViewModels;
using BankingSystem.ViewModels.OperationViewModels;
using BankingSystem.ViewModels.HistoryViewModels;
using BankingSystem.Models.Implementations.Data;
using BankingSystem.Models.Implementations.Data.Factories;
using System.Collections.ObjectModel;
using BankingSystem.Models.Implementations.Clients;
using BankingSystem.Models.Implementations.Data.DbInteraction;

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

        #region Модели представления
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

        public IRepository Repository { get; private set; }

        public Node SelectedNode { get; private set; }
        public ObservableCollection<Client> Clients { get; set; }

        #region Visibility

        #region ClientsLists
        public bool IndividualsVisibility
        {
            get
            {
                if (SelectedNode != null)
                    return SelectedNode.Type == NodeType.Individual || SelectedNode.Type == NodeType.VIPIndividual ? true : false;

                return false;
            }
        }

        public bool EntitiesVisibility
        {
            get
            {
                if (SelectedNode != null)
                    return SelectedNode.Type == NodeType.Entity || SelectedNode.Type == NodeType.VIPEntity ? true : false;

                return false;
            }
        }


        #endregion

        #endregion

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

        /// <summary>
        /// Команда при запуске приложения
        /// </summary>
        private ICommand windowLoaded;
        public ICommand WindowLoaded
        {
            get
            {
                return windowLoaded ??
                (windowLoaded = new RelayCommand(obj =>
                {
                    try
                    {
                        Repository = RepositoryFactory.CreateRepository(5);
                        OnPropertyChanged(nameof(Repository));
                    }
                    catch (Exception ex)
                    {
                        _messageService.ShowErrorMessage(_mainWindow, ex.Message);
                    }
                }));
            }
        }

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

        #region Команды главного окна

        /// <summary>
        /// Команда биндинга выборки при выборе узла дерева
        /// </summary>
        private ICommand selectedItemChangedCommand;
        public ICommand SelectedItemChangedCommand
        {
            get
            {
                return selectedItemChangedCommand ??
                (selectedItemChangedCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        SelectedNode = obj as Node;

                        switch (SelectedNode.Type)
                        {
                            case NodeType.Individual:
                                Clients = SelectClients.SelectIndividuals(); 

                                break;
                            case NodeType.Entity:
                                Clients = SelectClients.SelectEntities(); 
                                break;
                            case NodeType.VIPIndividual:
                                Clients = SelectClients.SelectVIPIndividuals();
                                break;
                            case NodeType.VIPEntity:
                                Clients = SelectClients.SelectVIPEntities();
                                break;
                            default:
                                Clients = null;
                                break;
                        }

                        OnPropertyChanged(nameof(Clients));
                        OnPropertyChanged(nameof(IndividualsVisibility));
                        OnPropertyChanged(nameof(EntitiesVisibility));
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
