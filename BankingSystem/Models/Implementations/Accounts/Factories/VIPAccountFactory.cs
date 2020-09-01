using BankingSystem.Models.Implementations.BankServices.CardService;
using BankingSystem.Models.Implementations.BankServices.DepositService;

namespace BankingSystem.Models.Implementations.Accounts.Factories
{
    class VIPAccountFactory : AccountFactory
    {
        public override Account CreateAccount(Card card, IDeposit deposit)
        {
            return new VipAccount(card, deposit);
        }
    }
}
