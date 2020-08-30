namespace BankingSystem.Models.Abstractions
{
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
