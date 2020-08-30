namespace BankingSystem.Models.Implementations.BankServices.CardService.Factories
{
    abstract class CardFactory
    {
        public abstract Card CreateCard(decimal cardBalance);
    }
}
