using helloserve.ExchangeRatesApi.Models;
using System;
using System.Threading.Tasks;

namespace helloserve.ExchangeRatesApi
{
    /// <summary>
    /// The abstraction for the Exchange Rates API client implementation
    /// </summary>
    public interface IExchangeRatesApiClient
    {
        /// <summary>
        /// Gets all the latest exchange rates of the supported currencies to convert to EUR
        /// </summary>
        /// <returns></returns>
        ExchangeRates GetLatestRates();
        /// <summary>
        /// Gets the latest exchange rates for only the specified currency codes to convert to EUR
        /// </summary>
        /// <param name="currencyCodes">A list of supported ISO currency codes</param>
        /// <returns></returns>
        ExchangeRates GetLatestRates(params string[] currencyCodes);
        /// <summary>
        /// Gets all the latest exchange rates to convert to the specified currency code
        /// </summary>
        /// <param name="baseCurrencyCode">A supported ISO currency code</param>
        /// <returns></returns>
        ExchangeRates GetLatestRates(string baseCurrencyCode);
        /// <summary>
        /// Gets the latest exchange rates for the specified currency codes to convert to the specified base currency code
        /// </summary>
        /// <param name="baseCurrencyCode">A supported ISO currency code</param>
        /// <param name="currencyCodes">A list of supported ISO currency codes</param>
        /// <returns></returns>
        ExchangeRates GetLatestRates(string baseCurrencyCode, params string[] currencyCodes);

        /// <summary>
        /// Gets all the latest exchange rates of the supported currencies to convert to EUR
        /// </summary>
        /// <returns></returns>
        Task<ExchangeRates> GetLatestRatesAsync();
        /// <summary>
        /// Gets the latest exchange rates for only the specified currency codes to convert to EUR
        /// </summary>
        /// <param name="currencyCodes">A list of supported ISO currency codes</param>
        /// <returns></returns>
        Task<ExchangeRates> GetLatestRatesAsync(params string[] currencyCodes);
        /// <summary>
        /// Gets all the latest exchange rates to convert to the specified currency code
        /// </summary>
        /// <param name="baseCurrencyCode">A supported ISO currency code</param>
        /// <returns></returns>
        Task<ExchangeRates> GetLatestRatesAsync(string baseCurrencyCode);
        /// <summary>
        /// Gets the latest exchange rates for the specified currency codes to convert to the specified base currency code
        /// </summary>
        /// <param name="baseCurrencyCode">A supported ISO currency code</param>
        /// <param name="currencyCodes">A list of supported ISO currency codes</param>
        /// <returns></returns>
        Task<ExchangeRates> GetLatestRatesAsync(string baseCurrencyCode, params string[] currencyCodes);

        /// <summary>
        /// Gets all the exchange rates reported on the specified date of the supported currencies to convert to EUR
        /// </summary>
        /// <param name="date">The reported date of the exchange rates</param>
        /// <returns></returns>
        ExchangeRates GetRatesForDate(DateTime date);
        /// <summary>
        /// Gets the exchange rates reported on the specified date for only the specified currency codes to convert to EUR
        /// </summary>
        /// <param name="date">The reported date of the exchange rates</param>
        /// <param name="currencyCodes">A list of supported ISO currency codes</param>
        /// <returns></returns>
        ExchangeRates GetRatesForDate(DateTime date, params string[] currencyCodes);
        /// <summary>
        /// Gets all the exchange rates reported at the specified date to convert to the specified currency code
        /// </summary>
        /// <param name="date">The reported date of the exchange rates</param>
        /// <param name="baseCurrencyCode">A supported ISO currency code</param>
        /// <returns></returns>
        ExchangeRates GetRatesForDate(DateTime date, string baseCurrencyCode);
        /// <summary>
        /// Gets the exchange rates reported at the specified date for the specified currency codes to convert to the specified base currency code
        /// </summary>
        /// <param name="date">The reported date of the exchange rates</param>
        /// <param name="baseCurrencyCode">A supported ISO currency code</param>
        /// <param name="currencyCodes">A list of supported ISO currency codes</param>
        /// <returns></returns>
        ExchangeRates GetRatesForDate(DateTime date, string baseCurrencyCode, params string[] currencyCodes);

        /// <summary>
        /// Gets all the exchange rates reported on the specified date of the supported currencies to convert to EUR
        /// </summary>
        /// <param name="date">The reported date of the exchange rates</param>
        /// <returns></returns>
        Task<ExchangeRates> GetRatesForDateAsync(DateTime date);
        /// <summary>
        /// Gets the exchange rates reported on the specified date for only the specified currency codes to convert to EUR
        /// </summary>
        /// <param name="date">The reported date of the exchange rates</param>
        /// <param name="currencyCodes">A list of supported ISO currency codes</param>
        /// <returns></returns>
        Task<ExchangeRates> GetRatesForDateAsync(DateTime date, params string[] currencyCodes);
        /// <summary>
        /// Gets all the exchange rates reported at the specified date to convert to the specified currency code
        /// </summary>
        /// <param name="date">The reported date of the exchange rates</param>
        /// <param name="baseCurrencyCode">A supported ISO currency code</param>
        /// <returns></returns>
        Task<ExchangeRates> GetRatesForDateAsync(DateTime date, string baseCurrencyCode);
        /// <summary>
        /// Gets the exchange rates reported at the specified date for the specified currency codes to convert to the specified base currency code
        /// </summary>
        /// <param name="date">The reported date of the exchange rates</param>
        /// <param name="baseCurrencyCode">A supported ISO currency code</param>
        /// <param name="currencyCodes">A list of supported ISO currency codes</param>
        /// <returns></returns>
        Task<ExchangeRates> GetRatesForDateAsync(DateTime date, string baseCurrencyCode, params string[] currencyCodes);

        /// <summary>
        /// Gets all the exchange rates reported for the specified period of the supported currencies to convert to EUR
        /// </summary>
        /// <param name="fromDate">The inclusive start date of the reporting period of the exchange rates</param>
        /// <param name="toDate">The include end date of the reporting period of the exchange rates</param>
        /// <returns></returns>
        ExchangeRatesRange GetRatesForDateRange(DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Gets the exchange rates reported for the specified period for only the specified currency codes to convert to EUR
        /// </summary>
        /// <param name="fromDate">The inclusive start date of the reporting period of the exchange rates</param>
        /// <param name="toDate">The include end date of the reporting period of the exchange rates</param>
        /// <param name="currencyCodes">A list of supported ISO currency codes</param>
        /// <returns></returns>
        ExchangeRatesRange GetRatesForDateRange(DateTime fromDate, DateTime toDate, params string[] currencyCodes);
        /// <summary>
        /// Gets all the exchange rates reported for the specified period to convert to the specified currency code
        /// </summary>
        /// <param name="fromDate">The inclusive start date of the reporting period of the exchange rates</param>
        /// <param name="toDate">The include end date of the reporting period of the exchange rates</param>
        /// <param name="baseCurrencyCode">A supported ISO currency code</param>
        /// <returns></returns>
        ExchangeRatesRange GetRatesForDateRange(DateTime fromDate, DateTime toDate, string baseCurrencyCode);
        /// <summary>
        /// Gets the exchange rates reported for the specified period for the specified currency codes to convert to the specified base currency code
        /// </summary>
        /// <param name="fromDate">The inclusive start date of the reporting period of the exchange rates</param>
        /// <param name="toDate">The include end date of the reporting period of the exchange rates</param>
        /// <param name="baseCurrencyCode">A supported ISO currency code</param>
        /// <param name="currencyCodes">A list of supported ISO currency codes</param>
        /// <returns></returns>
        ExchangeRatesRange GetRatesForDateRange(DateTime fromDate, DateTime toDate, string baseCurrencyCode, params string[] currencyCodes);

        /// <summary>
        /// Gets all the exchange rates reported for the specified period of the supported currencies to convert to EUR
        /// </summary>
        /// <param name="fromDate">The inclusive start date of the reporting period of the exchange rates</param>
        /// <param name="toDate">The include end date of the reporting period of the exchange rates</param>
        /// <returns></returns>
        Task<ExchangeRatesRange> GetRatesForDateRangeAsync(DateTime fromDate, DateTime toDate);
        /// <summary>
        /// Gets the exchange rates reported for the specified period for only the specified currency codes to convert to EUR
        /// </summary>
        /// <param name="fromDate">The inclusive start date of the reporting period of the exchange rates</param>
        /// <param name="toDate">The include end date of the reporting period of the exchange rates</param>
        /// <param name="currencyCodes">A list of supported ISO currency codes</param>
        /// <returns></returns>
        Task<ExchangeRatesRange> GetRatesForDateRangeAsync(DateTime fromDate, DateTime toDate, params string[] currencyCodes);
        /// <summary>
        /// Gets all the exchange rates reported for the specified period to convert to the specified currency code
        /// </summary>
        /// <param name="fromDate">The inclusive start date of the reporting period of the exchange rates</param>
        /// <param name="toDate">The include end date of the reporting period of the exchange rates</param>
        /// <param name="baseCurrencyCode">A supported ISO currency code</param>
        /// <returns></returns>
        Task<ExchangeRatesRange> GetRatesForDateRangeAsync(DateTime fromDate, DateTime toDate, string baseCurrencyCode);
        /// <summary>
        /// Gets the exchange rates reported for the specified period for the specified currency codes to convert to the specified base currency code
        /// </summary>
        /// <param name="fromDate">The inclusive start date of the reporting period of the exchange rates</param>
        /// <param name="toDate">The include end date of the reporting period of the exchange rates</param>
        /// <param name="baseCurrencyCode">A supported ISO currency code</param>
        /// <param name="currencyCodes">A list of supported ISO currency codes</param>
        /// <returns></returns>
        Task<ExchangeRatesRange> GetRatesForDateRangeAsync(DateTime fromDate, DateTime toDate, string baseCurrencyCode, params string[] currencyCodes);
    }
}
