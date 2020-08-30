using System.Windows;
using BankingSystem.ViewModels;
using BankingSystem.Models.Implementations;

namespace BankingSystem.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel(this, new FilePathService(), new MessageService());
        }
    }
}
