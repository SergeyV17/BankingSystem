using BankingSystem.Models.Implementations.Clients;

namespace BankingSystem.Models.Implementations.BankServices.DepositService
{
    /// <summary>
    /// Структура ставок для депозита
    /// </summary>
    struct DepositRates
    {
        private const double individualRate = 0.12;
        private const double entityRate = 0.10;

        /// <summary>
        /// Метод создания ставки для депозита
        /// </summary>
        /// <param name="clientType">тип клиента</param>
        /// <returns>ставка</returns>
        public static double GetDepositRate(ClientType clientType) => clientType == ClientType.Individual ? individualRate : entityRate;
    }
}
