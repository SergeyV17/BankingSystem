using System;
using System.Collections.Generic;

namespace BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories
{
    static class CardNameFactory
    {
        private enum CardNames
        {
            VisaClassic,
            VisaPlatinum,
            VisaBlack,
            VisaCorporate
        }

        private static readonly Random _random;
        private static readonly Dictionary<CardNames, string> _cardNames;

        static CardNameFactory()
        {
            _random = new Random();

            _cardNames = new Dictionary<CardNames, string>
            {
                [CardNames.VisaClassic] = "Visa Classic",
                [CardNames.VisaPlatinum] = "Visa Platinum",
                [CardNames.VisaBlack] = "Visa Black",
                [CardNames.VisaCorporate] = "Visa Corporate"
            };
        }

        /// <summary>
        /// Создание карты
        /// </summary>
        /// <returns>наименование карты</returns>
        public static string CreateCardName(bool isIndividual)
        {
            if (isIndividual)
            {
                int percent = _random.Next(101);

                if (percent < 60)
                    return _cardNames[CardNames.VisaClassic];
                else if (percent > 60 && percent < 80)
                    return _cardNames[CardNames.VisaPlatinum];
                else
                    return _cardNames[CardNames.VisaBlack];
            }

            return _cardNames[CardNames.VisaCorporate];
        }
    }
}
