using BankingSystem.Models.Implementations.Clients;

namespace BankingSystem.Models.Implementations.BankServices.DepositService
{
    /// <summary>
    /// Структура ставок для депозита
    /// </summary>
    struct DepositRates
    {
        private const decimal _individualRate = 0.12m;
        private const decimal _entityRate = 0.10m;

        /// <summary>
        /// Метод создания ставки для депозита
        /// </summary>
        /// <param name="clientType">тип клиента</param>
        /// <returns>ставка</returns>
        public static decimal GetDepositRate(ClientType clientType) => clientType == ClientType.Individual ? _individualRate : _entityRate;
    }
}
