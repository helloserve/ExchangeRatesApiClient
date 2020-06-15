using helloserve.ExchangeRatesApi.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.ExchangeRatesApi.Service
{
    public class ExchangeRateApiClient : IExchangeRatesApiClient
    {
        readonly ExchangeRatesApiOptions options;
        readonly ILogger<ExchangeRateApiClient> logger;
        internal static HttpClient httpClient;

        public ExchangeRateApiClient(ExchangeRatesApiOptions options, ILogger<ExchangeRateApiClient> logger = null, HttpClient httpClient = null)
        {
            this.options = options;
            this.logger = logger;
            ExchangeRateApiClient.httpClient = httpClient ?? (ExchangeRateApiClient.httpClient ?? new HttpClient());
        }

        public ExchangeRates GetLatestRates()
        {
            return GetLatestRates(baseCurrencyCode: null, currencyCodes: null);
        }

        public ExchangeRates GetLatestRates(params string[] currencyCodes)
        {
            return GetLatestRates(baseCurrencyCode: null, currencyCodes);
        }

        public ExchangeRates GetLatestRates(string baseCurrencyCode)
        {
            return GetLatestRates(baseCurrencyCode, currencyCodes: null);
        }

        public ExchangeRates GetLatestRates(string baseCurrencyCode, params string[] currencyCodes)
        {
            return GetLatestRatesAsync(baseCurrencyCode, currencyCodes).Result;
        }

        public Task<ExchangeRates> GetLatestRatesAsync()
        {
            return GetLatestRatesAsync(baseCurrencyCode: null, currencyCodes: null);
        }

        public Task<ExchangeRates> GetLatestRatesAsync(params string[] currencyCodes)
        {
            return GetLatestRatesAsync(baseCurrencyCode: null, currencyCodes);
        }

        public Task<ExchangeRates> GetLatestRatesAsync(string baseCurrencyCode)
        {
            return GetLatestRatesAsync(baseCurrencyCode, currencyCodes: null);
        }

        public Task<ExchangeRates> GetLatestRatesAsync(string baseCurrencyCode, params string[] currencyCodes)
        {
            return GetRatesForUri("latest", baseCurrencyCode, currencyCodes);
        }

        public ExchangeRates GetRatesForDate(DateTime date)
        {
            return GetRatesForDate(date, baseCurrencyCode: null, currencyCodes: null);
        }

        public ExchangeRates GetRatesForDate(DateTime date, params string[] currencyCodes)
        {
            return GetRatesForDate(date, baseCurrencyCode: null, currencyCodes);
        }

        public ExchangeRates GetRatesForDate(DateTime date, string baseCurrencyCode)
        {
            return GetRatesForDate(date, baseCurrencyCode, currencyCodes: null);
        }

        public ExchangeRates GetRatesForDate(DateTime date, string baseCurrencyCode, params string[] currencyCodes)
        {
            return GetRatesForDateAsync(date, baseCurrencyCode, currencyCodes).Result;
        }

        public Task<ExchangeRates> GetRatesForDateAsync(DateTime date)
        {
            return GetRatesForDateAsync(date, baseCurrencyCode: null, currencyCodes: null);
        }

        public Task<ExchangeRates> GetRatesForDateAsync(DateTime date, params string[] currencyCodes)
        {
            return GetRatesForDateAsync(date, baseCurrencyCode: null, currencyCodes);
        }

        public Task<ExchangeRates> GetRatesForDateAsync(DateTime date, string baseCurrencyCode)
        {
            return GetRatesForDateAsync(date, baseCurrencyCode, currencyCodes: null);
        }

        public Task<ExchangeRates> GetRatesForDateAsync(DateTime date, string baseCurrencyCode, params string[] currencyCodes)
        {
            return GetRatesForUri(date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture.DateTimeFormat), baseCurrencyCode, currencyCodes);
        }

        private async Task<ExchangeRates> GetRatesForUri(string relativeUri, string baseCurrencyCode, params string[] currencyCodes)
        {
            try
            {
                string query = string.Empty;
                if (!string.IsNullOrEmpty(baseCurrencyCode) || (currencyCodes is object && currencyCodes.Length > 0))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("?");
                    if (!string.IsNullOrEmpty(baseCurrencyCode))
                    {
                        builder.Append("base=");
                        builder.Append(baseCurrencyCode);
                    }
                    if (currencyCodes is object && currencyCodes.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(baseCurrencyCode))
                        {
                            builder.Append("&");
                        }
                        builder.Append("symbols=");
                        builder.Append(string.Join(",", currencyCodes));
                    }
                    query = builder.ToString();
                }
                Uri uri = new Uri(new Uri(options.ApiUrl), $"{relativeUri}{query}");
                var response = await httpClient.GetAsync(uri);
                using (Stream s = await response.Content.ReadAsStreamAsync())
                using (StreamReader reader = new StreamReader(s))
                using (JsonReader jreader = new JsonTextReader(reader))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    var result = serializer.Deserialize<ExchangeRates>(jreader);
                    return result;
                }
            }
            catch (HttpRequestException rex)
            {
                logger?.LogError(rex, rex.Message);
                throw;
            }
            catch (ArgumentOutOfRangeException aex)
            {
                logger?.LogError(aex, aex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex, "Unexpected error occured while retrieving exchange rates.");
                throw;
            }
        }
    }
}
