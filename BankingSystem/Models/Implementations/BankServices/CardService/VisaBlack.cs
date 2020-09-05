using BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories;

namespace BankingSystem.Models.Implementations.BankServices.CardService
{
    /// <summary>
    /// Карта "Visa black"
    /// </summary>
    class VisaBlack : Card
    {
        /// <summary>
        /// Конструктор "Visa black" карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        public VisaBlack(decimal cardBalance) : base("Visa black", CardNumberFactory.CreateCardNumber(), cardBalance)
        {
        }
    }
}
