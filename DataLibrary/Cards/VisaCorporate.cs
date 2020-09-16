using RequisitesLibrary.CardRequisites.Factories;

namespace DataLibrary.Cards
{
    /// <summary>
    /// Класс "Visa corporate" карты
    /// </summary>
    public class VisaCorporate : Card
    {
        /// <summary>
        /// Конструктор "Visa corporate" карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        public VisaCorporate(decimal cardBalance) : base("Visa Corporate", CardNumberFactory.CreateCardNumber(), cardBalance) { }
    }
}
