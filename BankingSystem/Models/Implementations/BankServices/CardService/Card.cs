namespace BankingSystem.Models.Implementations.BankServices.CardService
{
    abstract class Card
    {
        protected string CardName { get; }
        protected string CardNumber { get; }
        private decimal CardBalance { get; }

        protected Card(string cardName, string cardNumber, decimal cardBalance)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            CardBalance = cardBalance;
        }
    }
}
