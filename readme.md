# helloserve.ExchangeRatesApi

This is a .NET Standard 2.0 client for the https://exchangeratesapi.io/ service. Available on [Nuget](https://www.nuget.org/packages/helloserve.ExchangeRatesApi/).

## Usage

Add it:

```
services.AddExchangeRatesApiClient(config => 
{
    config.ApiKey = "<key from your config or user secrets>";
});
```

Inject it:

```
public MyClass(IExchangeRatesApiClient exchangeRatesClient)
{

}
```

Call it:

```
double usdEurRate = exchangeRatesClient.GetLatestRates().Rates["EUR"];
```

## Remarks

The `IExchangeRatesApiClient` interface provides methods and overloads to cover all the possible RESTfull route and query string combinations supported by the Exchange Rates API service.
It is important to note that, when retreiving rates for a date on which no rates were reported, you will be given the closest rate published before that date.
For example, if you request rates for 2017-12-17, you will instead get rates dated in the response to 2017-12-15. Additionally, when retreiving rates for a date range,
the result will not includes entries for dates on which no rates were reported.

## API Key

Version 1.1 adds support for the new `access_key` query parameter. The service (exchangeratesapi.io) recently expanded to a [multi-tier pricing structure](https://exchangeratesapi.io/pricing/), which requires you to register for an API key.

## Errors

The service recently changed to a success flag response model (instead of HTTP error codes), and so there is now an `ExchangeRateApiException` thrown when this flag is false. The `ErrorCode` property is enumerated according to their documentation, but it should be noted that not all errors have a code, and will be reported as _unknown_.

## Limitations

Their subscription tier limitations are reported through the error reponses, but broadly for the free tier (which I suspect is what most people are interested in): 

* no HTTPS support, so configure the API URL accordingly.
* doesn't support specifying a base currency, everything is reported in EUR.
* doesn't support the `/timeseries` endpoint, e.g. the `GetRatesForDateRangeAsync` method group.