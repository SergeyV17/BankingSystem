using BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories;

namespace BankingSystem.Models.Implementations.BankServices.CardService
{
    /// <summary>
    /// Класс "Visa classic" карты
    /// </summary>
    class VisaClassic : Card
    {
        /// <summary>
        /// Конструктор "Visa classic" карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        public VisaClassic(decimal cardBalance) : base("VISA CLASSIC", CardNumberFactory.CreateCardNumber(), cardBalance) { }
    }
}
