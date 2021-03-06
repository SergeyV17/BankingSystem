﻿
namespace CardsLibrary
{
    /// <summary>
    /// Класс карты
    /// </summary>
    public abstract class Card
    {
        /// <summary>
        /// Кoнструктор по умолчанию для EF
        /// </summary>
        public Card() { }

        /// <summary>
        /// Конструктор карты
        /// </summary>
        /// <param name="cardName">наименование</param>
        /// <param name="cardNumber">номер</param>
        /// <param name="cardBalance">баланс</param>
        protected Card(string cardName, string cardNumber, decimal cardBalance)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            CardBalance = cardBalance;
        }
            
        public int Id { get; set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public decimal CardBalance { get; set; }

        //Свойства для БД
        public Account Account { get; set; }
    }
}
