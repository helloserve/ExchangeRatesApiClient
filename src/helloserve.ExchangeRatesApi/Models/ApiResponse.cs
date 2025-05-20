using System;
using System.Collections.Generic;

namespace helloserve.ExchangeRatesApi.Models;

internal class ApiResponse
{
    public bool Success { get; set; }
    public ApiResponseError Error { get; set; }
    public string Base { get; set; }
}

internal class ApiResponseError
{
    public int Code { get; set; }
    public string Type { get; set; }
    public string Info { get; set; }
}

internal class ExchangeRatesResponse : ApiResponse
{
    public int? Timestamp { get; set; }
    public DateTime Date { get; set; }
    public RatesCollection Rates { get; set; }
}

internal class ExchangeRatesRangeResponse : ApiResponse
{
    public bool Timeseries { get; set; }
    public DateTime Start_Date { get; set; }
    public DateTime End_Date { get; set; }
    public Dictionary<DateTime, RatesCollection> Rates { get; set; }
}
