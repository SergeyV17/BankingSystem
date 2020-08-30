using BankingSystem.Models.Implementations.Clients;

namespace BankingSystem.Models.Implementations.BankServices.DepositService.Factories
{
    class DepositFactory
    {
        public static Deposit CreateDeposit(decimal depositBalance, bool depositCapitalization, ClientType clientType)
        {
            return new Deposit(depositBalance, depositCapitalization, DepositRates.GetDepositRate(clientType));
        }
    }
}
