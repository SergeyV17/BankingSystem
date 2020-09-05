namespace BankingSystem.Models.Abstractions
{
    /// <summary>
    /// Интерфейс предоставляющий путь к файлу
    /// </summary>
    interface IFilePathService
    {
        // путь к файлу
        string FilePath { get; set; }

        /// <summary>
        /// Метод получающий путь к файлу с информацией
        /// </summary>
        void GetPath(string filePath);
    }
}
