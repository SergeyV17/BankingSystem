using BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories;

namespace BankingSystem.Models.Implementations.BankServices.CardService
{
    /// <summary>
    /// Класс "Visa corporate" карты
    /// </summary>
    class VisaCorporate : Card
    {
        /// <summary>
        /// Конструктор "Visa corporate" карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        public VisaCorporate(decimal cardBalance) : base("Visa corporate", CardNumberFactory.CreateCardNumber(), cardBalance) { }
    }
}
