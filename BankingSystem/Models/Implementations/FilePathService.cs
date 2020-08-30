using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Models.Abstractions;

namespace BankingSystem.Models.Implementations
{
    /// <summary>
    /// Класс предоставляющий путь к папке решения
    /// </summary>
    class FilePathService : IFilePathService
    {
        // путь к файлу
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
