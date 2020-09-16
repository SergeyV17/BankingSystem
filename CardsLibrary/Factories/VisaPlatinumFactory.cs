namespace CardsLibrary.Factories
{
    /// <summary>
    /// Класс фабрики "Visa platinum" карты
    /// </summary>
    class VisaPlatinumFactory : CardFactory
    {
        /// <summary>
        /// Метод создания "Visa platinum" карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        /// <returns>"Visa platinum" карта</returns>
        public override Card CreateCard(decimal cardBalance) => new VisaPlatinum(cardBalance);
    }
}
