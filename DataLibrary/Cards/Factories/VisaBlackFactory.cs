﻿namespace DataLibrary.Cards.Factories
{
    /// <summary>
    /// Класс фабрики для "Visa black" карты
    /// </summary>
    public class VisaBlackFactory : CardFactory
    {
        /// <summary>
        /// Метод создания "Visa black" карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        /// <returns>"Visa black" карта</returns>
        public override Card CreateCard(decimal cardBalance) => new VisaBlack(cardBalance);
    }
}
