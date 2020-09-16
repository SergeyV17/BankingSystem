using System;
using System.Collections.Generic;

namespace RequisitesLibrary.CardRequisites.Factories
{
    /// <summary>
    /// Класс фабрики наименования карты
    /// </summary>
    public static class CardNameFactory
    {
        private static readonly Random random;
        public static Dictionary<CardNames, string> CardNamesDictionary { get; }

        /// <summary>
        /// Конструктор фабрики наименования карты
        /// </summary>
        static CardNameFactory()
        {
            random = new Random();

            CardNamesDictionary = new Dictionary<CardNames, string>
            {
                [CardNames.VisaClassic] = "Visa Classic",
                [CardNames.VisaPlatinum] = "Visa Platinum",
                [CardNames.VisaBlack] = "Visa Black",
                [CardNames.VisaCorporate] = "Visa Corporate"
            };
        }

        /// <summary>
        /// Метод создания карты
        /// </summary>
        /// <returns>наименование карты</returns>
        public static string CreateCardName(ClientType type)
        {
            if (type == ClientType.Individual)
            {
                int percent = random.Next(101);

                if (percent < 60)
                    return CardNamesDictionary[CardNames.VisaClassic];
                else if (percent > 60 && percent < 80)
                    return CardNamesDictionary[CardNames.VisaPlatinum];
                else
                    return CardNamesDictionary[CardNames.VisaBlack];
            }

            return CardNamesDictionary[CardNames.VisaCorporate];
        }
    }
}
