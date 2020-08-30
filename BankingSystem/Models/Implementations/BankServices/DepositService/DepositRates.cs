using BankingSystem.Models.Implementations.Clients;

namespace BankingSystem.Models.Implementations.BankServices.DepositService
{
    struct DepositRates
    {
        private const decimal _individualRate = 0.12m;
        private const decimal _entityRate = 0.10m;

        /// <summary>
        /// Метод создания ставки для вклада
        /// </summary>
        /// <param name="clientType"></param>
        /// <returns>ставка</returns>
        public static decimal GetDepositRate(ClientType clientType)
        {
            return clientType == ClientType.Individual ? _individualRate : _entityRate;
        }
    }
}
