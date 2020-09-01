﻿using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.BankServices.DepositService;

namespace BankingSystem.Models.Implementations.Accounts.Factories
{
    abstract class AccountFactory
    {
        public abstract Account CreateAccount(Card card, IDeposit deposit);
    }
}
