﻿using System;
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
using BankingSystem.Models;

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
        public ObservableCollection<Client> Clients { get; private set; }
        public Client SelectedClient
        {
            get => selectedClient;
            set
            {
                selectedClient = value;
                OnPropertyChanged(nameof(ClientPanelVisibility));
            }
        }
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
                        var addIndividualWindow = new AddIndividualWindow() { Owner = mainWindow, DataContext = AddIndividuaViewModel };

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
                        if (SelectedClient != null)
                        {
                            var addEntityWindow = new AddEntityWindow() { Owner = mainWindow, DataContext = EditEntityViewModel };
                        }
                        else
                        {
                            messageService.ShowErrorMessage(mainWindow, "Необходимо выбрать клиента.");
                        }
                    }));
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
                            if (SelectedClient != null)
                            {
                                using (AppDbContext context = new AppDbContext())
                                {
                                    context.Clients.Remove(SelectedClient);
                                    context.SaveChanges();
                                }

                                messageService.ShowInfoMessage(mainWindow, $"Клиент: {SelectedClient.Passport.FullName.Name} успешно удален.");
                                Clients.Remove(SelectedClient);
                            }
                            else
                            {
                                messageService.ShowErrorMessage(mainWindow, "Необходимо выбрать клиента.");
                            }
                        }
                        catch (Exception ex)
                        {
                            messageService.ShowErrorMessage(mainWindow, ex.Message);
                        }
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
                        CardPanelVisibility = true;
                        DepositPanelVisibility = false;
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
                        DepositPanelVisibility = true;
                        CardPanelVisibility = false;
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
