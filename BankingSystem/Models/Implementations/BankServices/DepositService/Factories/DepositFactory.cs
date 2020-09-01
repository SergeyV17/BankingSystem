using BankingSystem.Models.Implementations.Clients;

namespace BankingSystem.Models.Implementations.BankServices.DepositService.Factories
{
    abstract class DepositFactory
    {
        public abstract IDeposit CreateDeposit(decimal depositBalance, bool depositCapitalization, ClientType clientType);
    }
}
