namespace BankingSystem.Models.Implementations.BankServices.CardService.Factories
{
    class VisaClassicFactory : CardFactory
    {
        public override Card CreateCard(decimal cardBalance)
        {
            return new VisaClassic(cardBalance);
        }
    }
}
