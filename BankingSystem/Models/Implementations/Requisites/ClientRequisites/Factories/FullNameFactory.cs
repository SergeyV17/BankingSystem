using System;
using System.Text.RegularExpressions;

namespace BankingSystem.Models.Implementations.Requisites.ClientRequisites.Factories
{
    static class FullNameFactory
    {
        private static readonly Random _random;

        private static readonly string[] _maleFirstNames;
        private static readonly string[] _maleLastNames;
        private static readonly string[] _maleMiddleNames;

        private static readonly string[] _femaleFirstNames;
        private static readonly string[] _femaleLastNames;
        private static readonly string[] _femaleMiddleNames;

        static FullNameFactory()
        {
            _random = new Random();

            //Мужские
            _maleLastNames = new string[]
            {
                "Антонов", "Букин", "Владимиров", "Голик", "Дмитриченко", "Евдакимов", "Забалуев", "Иванов",
                "Кондратьев", "Лазутин", "Магамедов", "Никулин", "Овсянов", "Петров", "Рахманинов", "Соколов",
                "Трофимов", "Федосеев", "Хабибулин"
            };

            _maleFirstNames = new string[]
            {
                "Артем", "Борис", "Венцеслав", "Геннадий", "Джозеппо", "Евгений", "Зелимхан", "Иннокентий",
                "Константин", "Леонид", "Марат", "Николай", "Остап", "Петр", "Руслан", "Сергей", "Тимофей", "Федор",
                "Харитон"
            };

            _maleMiddleNames = new string[]
            {
                "Андреевич", "Богданович", "Вениаминович", "Геннадьевич", "Дмитриевич", "Евгеньевич", "Захарович",
                "Игоревич", "Константинович", "Леонидович", "Максимович", "Николаевич", "Олегович", "Петрович",
                "Романович", "Сергеевич", "Тихонович", "Федорович", "Харитонович", "Эдуардович"
            };

            //Женские
            _femaleLastNames = new string[]
            {
                "Антонова", "Борисова", "Владимирова", "Гришина", "Донская", "Енотова", "Жерибор", "Заболотная",
                "Иванькова", "Комарова", "Лопатина", "Мишурина", "Нетесова", "Обатурина", "Пархимович", "Решетникова",
                "Соколова", "Трофимова", "Федорова", "Чигрина"
            };

            _femaleFirstNames = new string[]
            {
                "Аделаида", "Белла", "Виктория", "Галина", "Дарья", "Евгения", "Жанна", "Зоя", "Инна", "Кристина",
                "Людмила", "Мария", "Наталья", "Оксана", "Полина", "Роза", "Софья", "Тамара", "Фаина", "Хлоя", "Шакира",
                "Эвелина", "Юлия", "Ярослава"
            };

            _femaleMiddleNames = new string[]
            {
                "Аркадьевна", "Богдановна", "Викторовна", "Георгиевна", "Дмитриевна", "Евгеньевна", "Захаровна",
                "Ивановна", "Константиновна", "Леонидовна", "Михайловна", "Николаевна", "Олеговна", "Петровна",
                "Романовна", "Сергеевна", "Тимофеевна", "Федоровна", "Эдуардовна"
            };
        }

        /// <summary>
        /// Создание полного имени
        /// </summary>
        /// <returns>полное имя</returns>
        public static FullName CreateFullName(Gender gender)
        {
            string lastName, firstName, middleName;

            switch (gender)
            {
                case Gender.Male:
                    lastName = _maleLastNames[_random.Next(_maleLastNames.Length)];
                    firstName = _maleFirstNames[_random.Next(_maleFirstNames.Length)];
                    middleName = _maleMiddleNames[_random.Next(_maleMiddleNames.Length)];

                    return new FullName(lastName, firstName, middleName);
                case Gender.Female:
                    lastName = _femaleLastNames[_random.Next(_femaleLastNames.Length)];
                    firstName = _femaleFirstNames[_random.Next(_femaleFirstNames.Length)];
                    middleName = _femaleMiddleNames[_random.Next(_femaleMiddleNames.Length)];

                    return new FullName(lastName, firstName, middleName);

                default:
                    throw new ArgumentOutOfRangeException(nameof(gender), gender, null);
            }
        }

        public static FullName CreateFullName(string lastName, string firstName, string middleName)
        {
            string pattern = @"^[a-zA-Z]+$";

            if (!Regex.IsMatch(lastName, pattern) && Regex.IsMatch(firstName, pattern) && Regex.IsMatch(middleName, pattern))
            {
                throw new ArgumentException(
                    $"Передача недопустимых аргументов в параметры. Проверьте: {nameof(lastName)} {nameof(firstName)} {nameof(middleName)}");

            }

            return new FullName(lastName, firstName, middleName);
        }
    }
}
