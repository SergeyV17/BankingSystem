using RequisitesLibrary.CardRequisites.Factories;

namespace DataLibrary.Cards
{
    /// <summary>
    /// Класс "Visa platinum" карты
    /// </summary>
    public class VisaPlatinum : Card
    {
        /// <summary>
        /// Конструктор "Visa platinum" карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        public VisaPlatinum(decimal cardBalance) : base("Visa Platinum", CardNumberFactory.CreateCardNumber(), cardBalance) { }
    }
}
