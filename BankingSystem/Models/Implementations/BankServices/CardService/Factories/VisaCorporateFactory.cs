namespace BankingSystem.Models.Implementations.BankServices.CardService.Factories
{
    class VisaCorporateFactory : CardFactory
    {
        public override Card CreateCard(decimal cardBalance)
        {
            return new VisaCorporate(cardBalance);
        }
    }
}
