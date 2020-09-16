
using RequisitesLibrary.CardRequisites.Factories;

namespace CardsLibrary
{
    /// <summary>
    /// Класс "Visa classic" карты
    /// </summary>
    public class VisaClassic : Card
    {
        /// <summary>
        /// Конструктор "Visa classic" карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        public VisaClassic(decimal cardBalance) : base("Visa Classic", CardNumberFactory.CreateCardNumber(), cardBalance) { }
    }
}
