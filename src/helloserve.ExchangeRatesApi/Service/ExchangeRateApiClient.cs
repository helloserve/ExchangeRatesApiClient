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
            return GetRatesForUriAsync<ExchangeRates>("latest", fromDate: null, toDate: null, baseCurrencyCode, currencyCodes);
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
            return GetRatesForUriAsync<ExchangeRates>(date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture.DateTimeFormat), fromDate: null, toDate: null, baseCurrencyCode, currencyCodes);
        }

        public ExchangeRatesRange GetRatesForDateRange(DateTime fromDate, DateTime toDate)
        {
            return GetRatesForDateRange(fromDate, toDate, baseCurrencyCode: null, currencyCodes: null);
        }

        public ExchangeRatesRange GetRatesForDateRange(DateTime fromDate, DateTime toDate, params string[] currencyCodes)
        {
            return GetRatesForDateRange(fromDate, toDate, baseCurrencyCode: null, currencyCodes);
        }

        public ExchangeRatesRange GetRatesForDateRange(DateTime fromDate, DateTime toDate, string baseCurrencyCode)
        {
            return GetRatesForDateRange(fromDate, toDate, baseCurrencyCode, currencyCodes: null);
        }

        public ExchangeRatesRange GetRatesForDateRange(DateTime fromDate, DateTime toDate, string baseCurrencyCode, params string[] currencyCodes)
        {
            return GetRatesForDateRangeAsync(fromDate, toDate, baseCurrencyCode, currencyCodes).Result;
        }

        public Task<ExchangeRatesRange> GetRatesForDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            return GetRatesForDateRangeAsync(fromDate, toDate, baseCurrencyCode: null, currencyCodes: null);
        }

        public Task<ExchangeRatesRange> GetRatesForDateRangeAsync(DateTime fromDate, DateTime toDate, params string[] currencyCodes)
        {
            return GetRatesForDateRangeAsync(fromDate, toDate, baseCurrencyCode: null, currencyCodes);
        }

        public Task<ExchangeRatesRange> GetRatesForDateRangeAsync(DateTime fromDate, DateTime toDate, string baseCurrencyCode)
        {
            return GetRatesForDateRangeAsync(fromDate, toDate, baseCurrencyCode, currencyCodes: null);
        }

        public async Task<ExchangeRatesRange> GetRatesForDateRangeAsync(DateTime fromDate, DateTime toDate, string baseCurrencyCode, params string[] currencyCodes)
        {
            var internalResult = await GetRatesForUriAsync<ExchangeRatesRange_Internal>("history", fromDate, toDate, baseCurrencyCode, currencyCodes);
            return new ExchangeRatesRange()
            {
                Base = internalResult.Base,
                StartAt = internalResult.Start_At,
                EndAt = internalResult.End_At,
                RateRanges = internalResult.Rates
            };
        }

        private async Task<T> GetRatesForUriAsync<T>(string relativeUri, DateTime? fromDate, DateTime? toDate, string baseCurrencyCode, params string[] currencyCodes)
        {
            try
            {
                string query = string.Empty;
                if ((fromDate.HasValue && toDate.HasValue) || !string.IsNullOrEmpty(baseCurrencyCode) || (currencyCodes is object && currencyCodes.Length > 0))
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("?");
                    int queryCount = 0;
                    if (fromDate.HasValue && toDate.HasValue)
                    {
                        builder.Append("start_at=");
                        builder.Append(fromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture.DateTimeFormat));
                        builder.Append("&");
                        builder.Append("end_at=");
                        builder.Append(toDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture.DateTimeFormat));
                        queryCount++;
                    }
                    if (!string.IsNullOrEmpty(baseCurrencyCode))
                    {
                        if (queryCount > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append("base=");
                        builder.Append(baseCurrencyCode);
                        queryCount++;
                    }
                    if (currencyCodes is object && currencyCodes.Length > 0)
                    {
                        if (queryCount > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append("symbols=");
                        builder.Append(string.Join(",", currencyCodes));
                        queryCount++;
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
                    var result = serializer.Deserialize<T>(jreader);
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
