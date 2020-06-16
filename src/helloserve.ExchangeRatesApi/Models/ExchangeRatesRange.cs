using System;
using System.Collections.Generic;

namespace helloserve.ExchangeRatesApi.Models
{
    public class ExchangeRatesRange
    {
        public string Base { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public Dictionary<DateTime, RatesCollection> RateRanges { get; set; }
    }

    internal class ExchangeRatesRange_Internal
    {
        public string Base { get; set; }
        public DateTime Start_At { get; set; }
        public DateTime End_At { get; set; }
        public Dictionary<DateTime, RatesCollection> Rates { get; set; }
    }
}
