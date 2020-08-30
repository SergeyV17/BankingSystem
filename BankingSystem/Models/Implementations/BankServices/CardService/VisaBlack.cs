using BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories;

namespace BankingSystem.Models.Implementations.BankServices.CardService
{
    class VisaBlack : Card
    {
        public VisaBlack(decimal cardBalance) : base("Visa black", CardNumberFactory.CreateCardNumber(), cardBalance)
        {
        }
    }
}
