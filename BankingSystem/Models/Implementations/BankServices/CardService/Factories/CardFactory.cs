using System;
using static BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories.CardNameFactory;

namespace BankingSystem.Models.Implementations.BankServices.CardService.Factories
{
    /// <summary>
    /// Класс фабрики карт
    /// </summary>
    abstract class CardFactory
    {
        /// <summary>
        /// Метод создания карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        /// <returns>карта</returns>
        public abstract Card CreateCard(decimal cardBalance);

        /// <summary>
        /// Метод создания карты по наименованию
        /// </summary>
        /// <param name="cardName">наименование</param>
        /// <param name="cardBalance">баланс</param>
        /// <returns>карта</returns>
        public static Card CreateCard(string cardName, decimal cardBalance)
        {
            Card card;

            if (cardName == CardNamesDictionary[CardNames.VisaClassic])
                card = new VisaClassicFactory().CreateCard(cardBalance);
            else if (cardName == CardNamesDictionary[CardNames.VisaBlack])
                card = new VisaBlackFactory().CreateCard(cardBalance);
            else if (cardName == CardNamesDictionary[CardNames.VisaPlatinum])
                card = new VisaPlatinumFactory().CreateCard(cardBalance);
            else if (cardName == CardNamesDictionary[CardNames.VisaCorporate])
                card = new VisaCorporateFactory().CreateCard(cardBalance);
            else
                throw new ArgumentException($"Передача недопустимого аргумента в параметры. Проверьте:{nameof(cardName)}", nameof(cardName));

            return card;
        }
    }
}
