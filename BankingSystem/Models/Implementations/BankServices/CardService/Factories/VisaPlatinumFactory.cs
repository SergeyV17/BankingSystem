namespace BankingSystem.Models.Implementations.BankServices.CardService.Factories
{
    class VisaPlatinumFactory : CardFactory
    {
        public override Card CreateCard(decimal cardBalance)
        {
            return new VisaPlatinum(cardBalance);
        }
    }
}
