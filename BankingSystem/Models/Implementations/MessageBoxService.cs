using System.IO;
using System.Text;
using System.Windows;
using BankingSystem.Models.Abstractions;

namespace BankingSystem.Models.Implementations
{
    /// <summary>
    /// Класс для работы с всплывающими сообщениями
    /// </summary>
    class MessageService : IMessageService
    {
        /// <summary>
        /// Метод вызывающий окно сообщения с информацией об ошибке
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="message">сообщение</param>
        public void ShowMessageFromFile(Window window, string filePath)
        {
            MessageBox.Show(window,
                File.ReadAllText(filePath, Encoding.Default),
                window.Title,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        /// <summary>
        /// Метод вызывающий окно сообщения с информацией об ошибке
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="message">сообщение</param>
        public void ShowInfoMessage(Window window, string message)
        {
            MessageBox.Show(window,
                message,
                window.Title,
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        /// <summary>
        /// Метод вызывающий окно сообщения с информацией об ошибке
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="message">сообщение</param>
        public void ShowErrorMessage(Window window, string message)
        {
            MessageBox.Show(window,
                message,
                window.Title,
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
