namespace BankingSystem.Models.Implementations.BankServices.CardService.Factories
{
    class VisaBlackFactory : CardFactory
    {
        public override Card CreateCard(decimal cardBalance)
        {
            return new VisaBlack(cardBalance);
        }
    }
}
