using BankingSystem.Models.Implementations.Accounts.Factories;
using BankingSystem.Models.Implementations.BankServices.CardService.Factories;

namespace BankingSystem.Models.Implementations.Data.Factories
{
    static class AppFactories
    {
        // массивы с фабриками для возможности выбора рандомной фабрики
        public static AccountFactory[] AccountFactories { get; }
        public static CardFactory[] IndividualsCardFactories { get; }
        public static CardFactory[] EntitiesCardFactories { get; }

        static AppFactories()
        {
            AccountFactories = new AccountFactory[]
            {
                new RegularAccountFactory(), new VipAccountFactory()
            };

            IndividualsCardFactories = new CardFactory[]
            {
                new VisaClassicFactory(), new VisaBlackFactory(), new VisaPlatinumFactory()
            };

            EntitiesCardFactories = new CardFactory[] { new VisaCorporateFactory() };
        }
    }
}
