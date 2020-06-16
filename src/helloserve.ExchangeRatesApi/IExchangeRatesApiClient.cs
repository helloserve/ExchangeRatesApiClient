﻿using helloserve.ExchangeRatesApi.Models;
using System;
using System.Threading.Tasks;

namespace helloserve.ExchangeRatesApi
{
    public interface IExchangeRatesApiClient
    {
        ExchangeRates GetLatestRates();
        ExchangeRates GetLatestRates(params string[] currencyCodes);
        ExchangeRates GetLatestRates(string baseCurrencyCode);
        ExchangeRates GetLatestRates(string baseCurrencyCode, params string[] currencyCodes);

        Task<ExchangeRates> GetLatestRatesAsync();
        Task<ExchangeRates> GetLatestRatesAsync(params string[] currencyCodes);
        Task<ExchangeRates> GetLatestRatesAsync(string baseCurrencyCode);
        Task<ExchangeRates> GetLatestRatesAsync(string baseCurrencyCode, params string[] currencyCodes);

        ExchangeRates GetRatesForDate(DateTime date);
        ExchangeRates GetRatesForDate(DateTime date, params string[] currencyCodes);
        ExchangeRates GetRatesForDate(DateTime date, string baseCurrencyCode);
        ExchangeRates GetRatesForDate(DateTime date, string baseCurrencyCode, params string[] currencyCodes);

        Task<ExchangeRates> GetRatesForDateAsync(DateTime date);
        Task<ExchangeRates> GetRatesForDateAsync(DateTime date, params string[] currencyCodes);
        Task<ExchangeRates> GetRatesForDateAsync(DateTime date, string baseCurrencyCode);
        Task<ExchangeRates> GetRatesForDateAsync(DateTime date, string baseCurrencyCode, params string[] currencyCodes);

        ExchangeRatesRange GetRatesForDateRange(DateTime fromDate, DateTime toDate);
        ExchangeRatesRange GetRatesForDateRange(DateTime fromDate, DateTime toDate, params string[] currencyCodes);
        ExchangeRatesRange GetRatesForDateRange(DateTime fromDate, DateTime toDate, string baseCurrencyCode);
        ExchangeRatesRange GetRatesForDateRange(DateTime fromDate, DateTime toDate, string baseCurrencyCode, params string[] currencyCodes);

        Task<ExchangeRatesRange> GetRatesForDateRangeAsync(DateTime fromDate, DateTime toDate);
        Task<ExchangeRatesRange> GetRatesForDateRangeAsync(DateTime fromDate, DateTime toDate, params string[] currencyCodes);
        Task<ExchangeRatesRange> GetRatesForDateRangeAsync(DateTime fromDate, DateTime toDate, string baseCurrencyCode);
        Task<ExchangeRatesRange> GetRatesForDateRangeAsync(DateTime fromDate, DateTime toDate, string baseCurrencyCode, params string[] currencyCodes);
    }
}
