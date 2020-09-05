using BankingSystem.Models.Implementations.Requisites.CardRequisites.Factories;

namespace BankingSystem.Models.Implementations.BankServices.CardService
{
    /// <summary>
    /// Класс "Visa platinum" карты
    /// </summary>
    class VisaPlatinum : Card
    {
        /// <summary>
        /// Конструктор "Visa platinum" карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        public VisaPlatinum(decimal cardBalance) : base("Visa platinum", CardNumberFactory.CreateCardNumber(), cardBalance)
        {
        }
    }
}
