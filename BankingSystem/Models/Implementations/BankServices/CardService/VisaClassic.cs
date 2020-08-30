using BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories;

namespace BankingSystem.Models.Implementations.BankServices.CardService
{
    class VisaClassic : Card
    {
        public VisaClassic(decimal cardBalance) : base("Visa classic", CardNumberFactory.CreateCardNumber(), cardBalance)
        {
        }
    }
}
