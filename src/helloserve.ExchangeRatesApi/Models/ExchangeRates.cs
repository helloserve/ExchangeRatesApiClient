using System;

namespace helloserve.ExchangeRatesApi.Models
{
    public class ExchangeRates
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public RatesCollection Rates { get; set; }
    }
}
