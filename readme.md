# helloserve.ExchangeRatesApi

This is a .NET Core 2.0 client for the https://exchangeratesapi.io/ service.
Coming to Nuget when I've finished preparing this.

## Usage

Add it:

```
services.AddExchangeRatesApiClient();
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
