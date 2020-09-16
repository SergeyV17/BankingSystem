namespace CardsLibrary.Factories
{
    /// <summary>
    /// Класс фабрики "Visa corporate" карты
    /// </summary>
    class VisaCorporateFactory : CardFactory
    {
        /// <summary>
        /// Метод создания "Visa corporate" карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        /// <returns>"Visa corporate" карта</returns>
        public override Card CreateCard(decimal cardBalance) => new VisaCorporate(cardBalance);
    }
}
