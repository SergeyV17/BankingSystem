namespace BankingSystem.Models.Implementations.BankServices.CardService
{
    /// <summary>
    /// Класс карты
    /// </summary>
    abstract class Card
    {
        protected string CardName { get; }
        protected string CardNumber { get; }
        private decimal CardBalance { get; }

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
    }
}
