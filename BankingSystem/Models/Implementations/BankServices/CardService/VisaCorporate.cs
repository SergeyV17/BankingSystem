using BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories;

namespace BankingSystem.Models.Implementations.BankServices.CardService
{
    class VisaCorporate : Card
    {
        public VisaCorporate(decimal cardBalance) : base("Visa corporate", CardNumberFactory.CreateCardNumber(), cardBalance)
        {
        }
    }
}
