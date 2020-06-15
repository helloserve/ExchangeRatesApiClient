using helloserve.ExchangeRatesApi;
using helloserve.ExchangeRatesApi.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddExchangeRatesApiClient(this IServiceCollection services)
        {
            return services.AddExchangeRatesApiClient(options => { });
        }

        public static IServiceCollection AddExchangeRatesApiClient(this IServiceCollection services, Action<ExchangeRatesApiOptions> config)
        {
            return services.AddTransient(typeof(IExchangeRatesApiClient), s =>
            {
                var options = new ExchangeRatesApiOptions();
                config(options);
                return new ExchangeRateApiClient(options, s.GetService<ILogger<ExchangeRateApiClient>>(), s.GetService<HttpClient>());
            });
        }
    }
}
