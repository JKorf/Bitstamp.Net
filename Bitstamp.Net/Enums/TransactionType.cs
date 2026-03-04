namespace Bitstamp.Net.Enums
{
    /// <summary>
    /// Transaction type
    /// </summary>
    public enum TransactionType
    {
        Deposit = 0,
        Withdrawal = 1,
        MarketTrade = 2,
        SubAccountTransfer = 14,
        CreditedWithStakedAssets = 25,
        SentAssetsToStaking = 26,
        StakingReward = 27,
        ReferralReward = 32,
        SettlementTransfer = 33,
        InternalAccountTransfer = 35,
        SmallBalanceConversion = 53,
        SmallBalanceToNothing = 55,
        DerivatesPeriodicSettlement = 58,
        InsuranceFundClaim = 59,
        InsuranceFundPremium = 60,
        CollateralLiquidation = 61,
    }
}
