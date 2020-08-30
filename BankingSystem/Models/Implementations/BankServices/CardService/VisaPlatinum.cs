using BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories;

namespace BankingSystem.Models.Implementations.BankServices.CardService
{
    class VisaPlatinum : Card
    {
        public VisaPlatinum(decimal cardBalance) : base("Visa platinum", CardNumberFactory.CreateCardNumber(), cardBalance)
        {
        }
    }
}
