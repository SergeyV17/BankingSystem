using static RequisitesLibrary.CardRequisites.Factories.CardNameFactory;
using RequisitesLibrary.CardRequisites;

namespace CardsLibrary.Factories
{
    /// <summary>
    /// Класс создания карты в зависимости от имени карты
    /// </summary>
    class SimpleCardFactory
    {
        /// <summary>
        /// Метод создания карты по наименованию
        /// </summary>
        /// <param name="cardName">наименование</param>
        /// <param name="cardBalance">баланс</param>
        /// <returns>карта</returns>
        public static Card CreateCard(string cardName, decimal cardBalance)
        {
            Card card = null;

            if (cardName == CardNamesDictionary[CardNames.VisaClassic])
                card = new VisaClassicFactory().CreateCard(cardBalance);
            else if (cardName == CardNamesDictionary[CardNames.VisaBlack])
                card = new VisaBlackFactory().CreateCard(cardBalance);
            else if (cardName == CardNamesDictionary[CardNames.VisaPlatinum])
                card = new VisaPlatinumFactory().CreateCard(cardBalance);
            else if (cardName == CardNamesDictionary[CardNames.VisaCorporate])
                card = new VisaCorporateFactory().CreateCard(cardBalance);

            return card;
        }
    }
}
