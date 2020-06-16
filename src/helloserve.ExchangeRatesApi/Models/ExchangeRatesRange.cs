using System;
using System.Collections.Generic;

namespace helloserve.ExchangeRatesApi.Models
{
    /// <summary>
    /// Reports the exchange rates for a base currency for the specified date range
    /// </summary>
    public class ExchangeRatesRange
    {
        /// <summary>
        /// The currency that these exchange rates can be used to convert to
        /// </summary>
        public string Base { get; set; }
        /// <summary>
        /// The start of the period of the exchange rates
        /// </summary>
        public DateTime StartAt { get; set; }
        /// <summary>
        /// The end of the period of the exchange rates
        /// </summary>
        public DateTime EndAt { get; set; }
        /// <summary>
        /// The collection of dates in the range and each one's set of rates
        /// </summary>
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
