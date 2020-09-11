using System.Windows;

namespace BankingSystem.Models.Abstractions
{
    /// <summary>
    /// Интерфейс для работы с всплывающими сообщениями
    /// </summary>
    interface IMessageService
    {
        /// <summary>
        /// Метод вызывающий окно сообщения с информацией из файла
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="filePath">сообщение</param>
        void ShowMessageFromFile(Window window, string filePath);

        /// <summary>
        /// Метод вызывающий окно сообщения с информацией
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="message">сообщение</param>
        void ShowInfoMessage(Window window, string message);

        /// <summary>
        /// Метод вызывающий окно сообщения с предупреждением
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="message">сообщение</param>
        void ShowWarningMessage(Window window, string message);

        /// <summary>
        /// Метод вызывающий окно сообщения с информацией об ошибке
        /// </summary>
        /// <param name="window">текущее окно</param>
        /// <param name="message">сообщение</param>
        void ShowErrorMessage(Window window, string message);
    }
}
