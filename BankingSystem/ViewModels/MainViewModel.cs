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
using BankingSystem.Models.Implementations.Data.DbInteraction.ClientBaseEditing;
using BankingSystem.Views.Windows.OperationPanel;
using BankingSystem.Models.Implementations.Data.DbInteraction.Selections;
using BankingSystem.Models.Implementations.Data.DbInteraction.DepositOperations;

namespace BankingSystem.ViewModels
{
    /// <summary>
    /// Модель представление главного окна
    /// </summary>
    class MainViewModel : ViewModelBase
    {
        #region Поля

        private readonly MainWindow mainWindow;

        private readonly IFilePathService filePathService;
        private readonly IMessageService messageService;

        private Client selectedClient;
        private bool cardPanelVisibility;
        private bool depositPanelVisibility;

        #endregion

        #region Свойства

        #region Модели представления
        public EditIndividualViewModel EditIndividualViewModel { get; private set; }
        public EditEntityViewModel EditEntityViewModel { get; private set; }

        public ReplenishCardViewModel ReplenishmentCardViewModel { get; private set; }
        public TransferViewModel TransferViewModel { get; private set; }
        public OpenDeposiViewModel OpenDeposiViewModel { get; private set; }

        public ClientHistoryViewModel ClientHistoryViewModel { get; private set; }
        public OperationHistoryViewModel OperationHistoryViewModel { get; private set; }

        #endregion

        public IRepository Repository { get; private set; }

        public Node SelectedNode { get; private set; }
        public Client SelectedClient
        {
            get => selectedClient;
            set
            {
                selectedClient = value;
                OnPropertyChanged(nameof(ClientPanelVisibility));
            }
        }
        public ObservableCollection<Client> Clients { get; private set; }

        #region Visibility

        public bool LoadingPanelVisibility { get; private set; } // в разработке (требуется многопоточность)

        public bool ClientsVisibility => SelectedNode != null ? SelectedNode.Type != NodeType.Intermediate ? true : false : false;
        public bool ClientPanelVisibility => SelectedClient != null ? true : false;

        public bool CardPanelVisibility 
        { 
            get => cardPanelVisibility; 
            private set 
            { 
                cardPanelVisibility = value; 
                OnPropertyChanged(nameof(CardPanelVisibility)); 
            } 
        }

        public bool DepositPanelVisibility
        {
            get => depositPanelVisibility;
            private set
            {
                depositPanelVisibility = value;
                OnPropertyChanged(nameof(DepositPanelVisibility));
            }
        }

        public bool OpenDepositBtnVisibility => SelectedClient != null ? SelectedClient.Account.Deposit.HasDeposit ? false : true : false;
        public bool DepositInfoVisibility => SelectedClient != null ? SelectedClient.Account.Deposit.HasDeposit ? true : false : false;

        #endregion

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор модели представления главного окна
        /// </summary>
        /// <param name="dialogService"><сервис диалоговых окон/param>
        public MainViewModel(MainWindow mainWindow, IFilePathService filePathService, IMessageService messageService)
        {
            this.mainWindow = mainWindow;

            this.filePathService = filePathService;
            this.messageService = messageService;
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
                        Repository = RepositoryFactory.CreateRepository(20);
                        OnPropertyChanged(nameof(Repository));
                    }
                    catch (Exception ex)
                    {
                        messageService.ShowErrorMessage(mainWindow, ex.Message);
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
            get { return exitCommand ?? (exitCommand = new RelayCommand(obj => { mainWindow.Close(); })); }
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
                        filePathService.GetPath(@"Views\Resources\Text\InformationSheet.txt");
                        messageService.ShowMessageFromFile(mainWindow, filePathService.FilePath);
                    }
                    catch (Exception ex)
                    {
                        messageService.ShowErrorMessage(mainWindow, ex.Message);
                    }
                }));
            }
        }

        #endregion

        #region Команды главного окна

        /// <summary>
        /// Команда биндинга выборки при выборе узла дерева
        /// </summary>
        private ICommand selectedTreeItemChangedCommand;
        public ICommand SelectedTreeItemChangedCommand
        {
            get
            {
                return selectedTreeItemChangedCommand ??
                (selectedTreeItemChangedCommand = new RelayCommand(obj =>
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

                        CardPanelVisibility = false;
                        DepositPanelVisibility = false;

                        OnPropertyChanged(nameof(Clients));
                        OnPropertyChanged(nameof(ClientsVisibility));
                    }
                    catch (Exception ex)
                    {
                        messageService.ShowErrorMessage(mainWindow, ex.Message);
                    }
                }));
            }
        }

        /// <summary>
        /// Команда обработки при выборе клиента
        /// </summary>
        private ICommand selectedClientChangedCommand;
        public ICommand SelectedClientChangedCommand
        {
            get
            {
                return selectedClientChangedCommand ??
                (selectedClientChangedCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        OnPropertyChanged(nameof(OpenDepositBtnVisibility));
                        OnPropertyChanged(nameof(DepositInfoVisibility));
                    }
                    catch (Exception ex)
                    {
                        messageService.ShowErrorMessage(mainWindow, ex.Message);
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
                        switch (SelectedNode.Type)
                        {
                            case NodeType.Individual:
                            case NodeType.VIPIndividual:
                                var addIndividualWindow = new AddIndividualWindow(){ Owner = mainWindow };
                                addIndividualWindow.DataContext = new AddIndividualViewModel(addIndividualWindow, messageService, SelectedNode.IsVIP);

                                addIndividualWindow.ShowDialog();
                                break;
                            case NodeType.Entity:
                            case NodeType.VIPEntity:
                                var addEntityWindow = new AddEntityWindow() { Owner = mainWindow };
                                addEntityWindow.DataContext = new AddEntityViewModel(addEntityWindow, messageService, SelectedNode.IsVIP);

                                addEntityWindow.ShowDialog();
                                break;
                        }

                        selectedTreeItemChangedCommand = null;
                        SelectedTreeItemChangedCommand.Execute(SelectedNode);
                    },
                    (obj) => SelectedNode != null ? SelectedNode.Type != NodeType.Intermediate ? true : false : false));
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
                        switch (SelectedNode.Type)
                        {
                            case NodeType.Individual:
                            case NodeType.VIPIndividual:
                                var editIndividualWindow = new EditIndividualWindow() { Owner = mainWindow };
                                editIndividualWindow.DataContext = new EditIndividualViewModel(editIndividualWindow, messageService, SelectedClient as Individual);

                                editIndividualWindow.ShowDialog();
                                break;
                            case NodeType.Entity:
                            case NodeType.VIPEntity:
                                var editEntityWindow = new EditEntityWindow() { Owner = mainWindow };
                                editEntityWindow.DataContext = new EditEntityViewModel(editEntityWindow, messageService, SelectedClient as Entity);

                                editEntityWindow.ShowDialog();
                                break;
                        }

                        selectedTreeItemChangedCommand = null;
                        SelectedTreeItemChangedCommand.Execute(SelectedNode);
                    },
                    (obj) => SelectedClient != null));
            }
        }

        private ICommand removeClientCommand;
        public ICommand RemoveClientCommand
        {
            get
            {
                return removeClientCommand ??
                    (removeClientCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            var (success, message) = DeleteClient.DeleteClientFromDb(SelectedClient);

                            if (success)
                            {
                                messageService.ShowInfoMessage(mainWindow, message);
                                Clients.Remove(SelectedClient);
                            }
                            else
                            {
                                messageService.ShowWarningMessage(mainWindow, message);
                            }
                        }
                        catch (Exception ex)
                        {
                            messageService.ShowErrorMessage(mainWindow, ex.Message);
                        }
                    },
                    (obj) => SelectedClient != null));
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
                        CardPanelVisibility = true;
                        DepositPanelVisibility = false;
                    },
                    (obj) => SelectedClient != null));
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
                        DepositPanelVisibility = true;
                        CardPanelVisibility = false;
                    },
                    (obj) => SelectedClient != null));
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
                        var replenishCardWindow = new ReplenishCardWindow() { Owner = mainWindow };
                        replenishCardWindow.DataContext = new ReplenishCardViewModel(replenishCardWindow, messageService, SelectedClient);

                        replenishCardWindow.ShowDialog();

                        selectedTreeItemChangedCommand = null;
                        SelectedTreeItemChangedCommand.Execute(SelectedNode);
                    },
                    (obj) => SelectedClient != null));
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
                        var transferWindow = new TransferWindow() { Owner = mainWindow };
                        transferWindow.DataContext = new TransferViewModel(transferWindow, messageService, SelectedClient);

                        transferWindow.ShowDialog();

                        selectedTreeItemChangedCommand = null;
                        SelectedTreeItemChangedCommand.Execute(SelectedNode);
                    },
                    (obj) => SelectedClient != null ? SelectedClient.Account.Card.CardBalance <= 0 ? false : true : false));
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
                        var openDepositWindow = new OpenDepositWindow() { Owner = mainWindow };
                        openDepositWindow.DataContext = new OpenDeposiViewModel(openDepositWindow, messageService, SelectedClient);

                        openDepositWindow.ShowDialog();

                        selectedTreeItemChangedCommand = null;
                        SelectedTreeItemChangedCommand.Execute(SelectedNode);
                    },
                    (obj) => SelectedClient != null ? SelectedClient.Account.Card.CardBalance >= 20_000 ? true : false : false));
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
                        try
                        {
                            var(success, message) = СloseDeposit.Close(SelectedClient);

                            if (success)
                            {
                                messageService.ShowInfoMessage(mainWindow, message);
                            }
                            else
                            {
                                messageService.ShowWarningMessage(mainWindow, message);
                            }
                        }
                        catch (Exception ex)
                        {
                            messageService.ShowErrorMessage(mainWindow, ex.Message);
                        }

                        selectedTreeItemChangedCommand = null;
                        SelectedTreeItemChangedCommand.Execute(SelectedNode);
                    },
                    (obj) => SelectedClient != null ? SelectedClient.Account.Deposit.HasDeposit ? true : false : false));
            }
        }

        #endregion

        #endregion
    }
}
