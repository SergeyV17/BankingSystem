namespace BankingSystem.Models.Implementations.BankServices.CardService.Factories
{
    /// <summary>
    /// Класс фабрики для "Visa black" карты
    /// </summary>
    class VisaBlackFactory : CardFactory
    {
        /// <summary>
        /// Метод создания "Visa black" карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        /// <returns>"Visa black" карта</returns>
        public override Card CreateCard(decimal cardBalance) => new VisaBlack(cardBalance);
    }
}
