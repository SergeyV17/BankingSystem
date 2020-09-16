namespace DataLibrary.Cards.Factories
{
    /// <summary>
    /// Класс фабрики карт
    /// </summary>
    public abstract class CardFactory
    {
        /// <summary>
        /// Метод создания карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        /// <returns>карта</returns>
        public abstract Card CreateCard(decimal cardBalance);
    }
}
