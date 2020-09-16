using DataLibrary.Cards;
using DataLibrary.Clients;
using DataLibrary.Deposits;
using System;

namespace DataLibrary.Accounts
{
    /// <summary>
    /// Класс аккаунта
    /// </summary>
    public abstract class Account
    {
        private decimal amountOfReplenishmentPerDay;

        /// <summary>
        /// Конструктор по умолчанию для EF
        /// </summary>
        public Account() { }

        /// <summary>
        /// Конструктор аккаунта
        /// </summary>
        /// <param name="card">карта</param>
        /// <param name="deposit">депозит</param>
        protected Account(Card card, Deposit deposit)
        {
            Card = card;
            Deposit = deposit;

            AccountLockout = default;
            AmountOfReplenishmentPerDay = default;
        }

        public int Id { get; set; }
        public Card Card { get; set; }
        public Deposit Deposit { get; set; }
        public bool AccountLockout { get; set; }
        public decimal AmountOfReplenishmentPerDay
        {
            get => amountOfReplenishmentPerDay;

            set
            {
                if (DateOfLastReplenish != null && DateTime.Today != DateOfLastReplenish)
                {
                    amountOfReplenishmentPerDay = 0;
                }

                amountOfReplenishmentPerDay = value;
                DateOfLastReplenish = DateTime.Today;
            }
        }
        public decimal ReplenishementPerDayLimit => 300_000;
        public DateTime DateOfLastReplenish { get; set; }

        ////Свойства для БД
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
