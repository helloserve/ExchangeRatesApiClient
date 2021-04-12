namespace helloserve.ExchangeRatesApi
{
    public enum ExchangeRatesApiErrorCode
    {
        Unknown = 0,
        NoApiKey = 101,
        AccountInactive = 102,
        EndPointNotFound = 103,
        MaximumRequestVolumeReached = 104,
        SubscriptionDoesNotSupportCall = 105,
        NoResults = 106,
        InvalidBaseCurrency = 201,
        InvalidSymbol = 202,
        NoDate = 301,
        InvalidDate = 302,
        NoneOrInvalidAmount = 403,
        NoneOrInvalidTimeframe = 501,
        NoneOrInvalidStartDate = 502,
        NoneOrInvalidEndDate = 503,
        InvalidTimeframe = 504,
        TimeframeExceedsMaximum = 505
    }
}
