namespace CardsLibrary.Factories
{
    /// <summary>
    /// Класс фабрики "Visa classic" карты
    /// </summary>
    class VisaClassicFactory : CardFactory
    {
        /// <summary>
        /// Метод создания "Visa classic" карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        /// <returns>"Visa classic" карта</returns>
        public override Card CreateCard(decimal cardBalance) => new VisaClassic(cardBalance);
    }
}
