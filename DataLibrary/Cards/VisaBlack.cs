using RequisitesLibrary.CardRequisites.Factories;

namespace DataLibrary.Cards
{
    /// <summary>
    /// Карта "Visa black"
    /// </summary>
    public class VisaBlack : Card
    {
        /// <summary>
        /// Конструктор "Visa black" карты
        /// </summary>
        /// <param name="cardBalance">баланс</param>
        public VisaBlack(decimal cardBalance) : base("Visa Black", CardNumberFactory.CreateCardNumber(), cardBalance) { }
    }
}
