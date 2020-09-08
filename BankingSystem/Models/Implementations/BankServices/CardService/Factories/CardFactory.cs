using System;

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
            switch (cardName)
            {
                case "Visa Classic":
                    card = new VisaClassicFactory().CreateCard(cardBalance);
                    break;
                case "Visa Black":
                    card = new VisaBlackFactory().CreateCard(cardBalance);
                    break;
                case "Visa Platinum":
                    card = new VisaPlatinumFactory().CreateCard(cardBalance);
                    break;
                case "Visa Сorporate":
                    card = new VisaCorporateFactory().CreateCard(cardBalance);
                    break;
                default:
                    throw new ArgumentException($"Передача недопустимого аргумента в параметры. Проверьте:{nameof(cardName)}", nameof(cardName));
            }

            return card;
        }
    }
}
