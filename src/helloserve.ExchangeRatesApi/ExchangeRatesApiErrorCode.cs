namespace helloserve.ExchangeRatesApi;

public enum ExchangeRatesApiErrorCode
{
    Unknown = 0,
    NoApiKey = 101,
    AccountInactive = 102,
    EndPointNotFound = 103,
    MaximumRequestVolumeReached = 104,
    SubscriptionDoesNotSupportCall = 105,
    /// <summary>
    /// The request was successful, but no results were returned. This result appears to also be given when you are rate limited - e.g. making too many requests in quick succession.
    /// </summary>
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
