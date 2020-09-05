using System;
using System.IO;
using BankingSystem.Models.Abstractions;

namespace BankingSystem.Models.Implementations
{
    /// <summary>
    /// Класс предоставляющий путь к папке решения
    /// </summary>
    class FilePathService : IFilePathService
    {
        public string FilePath { get; set; }

        /// <summary>
        /// Метод получающий путь к файлу с информацией
        /// </summary>
        /// <param name="filePath">путь к файлу</param>
        public void GetPath(string filePath)
        {
            FilePath = Path.Combine(Path.GetDirectoryName(Directory.GetParent(Environment.CurrentDirectory).ToString()), filePath);
        }
    }
}
