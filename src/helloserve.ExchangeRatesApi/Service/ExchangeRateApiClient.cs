using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using helloserve.ExchangeRatesApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace helloserve.ExchangeRatesApi.Service;

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

    public async Task<ExchangeRates> GetLatestRatesAsync(string baseCurrencyCode, params string[] currencyCodes)
    {
        var response = await GetRatesForUriAsync<ExchangeRatesResponse>("latest", fromDate: null, toDate: null, baseCurrencyCode, currencyCodes).ConfigureAwait(false);
        return new ExchangeRates(response);
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

    public async Task<ExchangeRates> GetRatesForDateAsync(DateTime date, string baseCurrencyCode, params string[] currencyCodes)
    {
        var response = await GetRatesForUriAsync<ExchangeRatesResponse>(date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture.DateTimeFormat), fromDate: null, toDate: null, baseCurrencyCode, currencyCodes)
            .ConfigureAwait(false);
        return new ExchangeRates(response);
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
        var response = await GetRatesForUriAsync<ExchangeRatesRangeResponse>("timeseries", fromDate, toDate, baseCurrencyCode, currencyCodes).ConfigureAwait(false);
        return new ExchangeRatesRange(response);
    }

    private async Task<T> GetRatesForUriAsync<T>(string relativeUri, DateTime? fromDate, DateTime? toDate, string baseCurrencyCode, params string[] currencyCodes)
        where T : ApiResponse
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
            queryParameters.Add(new KeyValuePair<string, string>("access_key", options.ApiKey));

            if ((fromDate.HasValue && toDate.HasValue) || !string.IsNullOrEmpty(baseCurrencyCode) || (currencyCodes is object && currencyCodes.Length > 0))
            {
                if (fromDate.HasValue && toDate.HasValue)
                {
                    queryParameters.Add(new KeyValuePair<string, string>("start_date", fromDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture.DateTimeFormat)));
                    queryParameters.Add(new KeyValuePair<string, string>("end_date", toDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture.DateTimeFormat)));
                }

                if (!string.IsNullOrEmpty(baseCurrencyCode))
                {
                    queryParameters.Add(new KeyValuePair<string, string>("base", baseCurrencyCode));
                }

                if (currencyCodes is object && currencyCodes.Length > 0)
                {
                    queryParameters.Add(new KeyValuePair<string, string>("symbols", string.Join(",", currencyCodes)));
                }
            }
            string query = QueryString.Create(queryParameters).Value;
            Uri uri = new Uri(new Uri(options.ApiUrl), $"{relativeUri}{query}");
            logger?.LogInformation($"GET {uri.AbsoluteUri}");
            var response = await httpClient.GetAsync(uri).ConfigureAwait(false);
            using Stream s = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.Strict
            };
            var result = await JsonSerializer.DeserializeAsync<T>(s, jsonOptions).ConfigureAwait(false);
            if (!result.Success)
            {
                throw ExchangeRatesApiException.CreateFromResponse(result.Error);
            }
            return result;
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
        catch (ExchangeRatesApiException xex)
        {
            logger?.LogError(xex, $"ExchangeRateApi Exception Occured: {xex.ErrorCode} {xex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Unexpected error occured while retrieving exchange rates.");
            throw;
        }
        finally
        {
            stopwatch.Stop();
            logger?.LogDebug($"GET completed in {stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
