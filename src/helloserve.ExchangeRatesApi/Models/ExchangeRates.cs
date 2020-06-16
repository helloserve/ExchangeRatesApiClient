using System;

namespace helloserve.ExchangeRatesApi.Models
{
    /// <summary>
    /// Reports the exchange rates for a base currency and a specific date
    /// </summary>
    public class ExchangeRates
    {
        /// <summary>
        /// The currency that these rates can be used to convert from
        /// </summary>
        public string Base { get; set; }
        /// <summary>
        /// The date that these rates were reported
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// The collection of rates
        /// </summary>
        public RatesCollection Rates { get; set; }
    }
}
