using BankingSystem.Models.Implementations.Clients;

namespace BankingSystem.Models.Implementations.BankServices.DepositService.Factories
{
    class DefaultDepositFactory : DepositFactory
    {
        public override IDeposit CreateDeposit(decimal depositBalance, bool depositCapitalization, ClientType clientType)
        {
            return new Deposit(depositBalance, depositCapitalization, DepositRates.GetDepositRate(clientType));
        }
    }
}
