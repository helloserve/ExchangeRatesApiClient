using helloserve.ExchangeRatesApi;
using helloserve.ExchangeRatesApi.Models;
using helloserve.ExchangeRatesApi.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace helloserve.Test.ExchangeRateApi
{
    [TestClass]
    public class ExchangeRatesApiClientTests
    {
        private const string exchangeRatesHttpResult = "{ \"rates\": { \"EUR\": 0.0923668071, \"JPY\": 12.2302889234, \"USD\": 0.1337563733, \"GBP\": 0.0828714993 }, \"base\": \"ZAR\", \"date\": \"2010-01-12\" }";
        private const string exchangeRatesRangeHttpResult = "{\"rates\":{\"2018-01-03\":{\"JPY\":9.0678222312,\"ILS\":0.2794047499},\"2018-01-02\":{\"JPY\":9.0838926174,\"ILS\":0.2798187919}},\"start_at\":\"2017-12-31\",\"base\":\"ZAR\",\"end_at\":\"2018-01-03\"}";

        private readonly ExchangeRatesApiOptions options = new ExchangeRatesApiOptions();
        private HttpClient httpClient;

        protected IExchangeRatesApiClient Client => new ExchangeRateApiClient(options, httpClient: httpClient);

        internal TestHttpMessageHandler Initialize(Func<string> resultFunc)
        {
            var handler = new TestHttpMessageHandler(resultFunc);
            httpClient = new HttpClient(handler);
            return handler;
        }

        [TestMethod]
        public void GetLatestRates_Verify()
        {
            //arrange
            var handler = Initialize(null);

            //act
            Client.GetLatestRates();

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/latest", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public void GetLatestRates_WithBaseCurrency_Verify()
        {
            //arrange
            var handler = Initialize(null);
            string baseCurrency = "USD";

            //act
            Client.GetLatestRates(baseCurrency);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/latest?base=USD", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public void GetLatestRates_WithSymbols_Verify()
        {
            //arrange
            var handler = Initialize(null);
            string[] symbols = new string[] { "USD", "GBP" };

            //act
            Client.GetLatestRates(symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/latest?symbols=USD,GBP", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public void GetLatestRates_WithBaseCurrencyAndSymbols_Verify()
        {
            //arrange
            var handler = Initialize(null);
            string baseCurrency = "ZAR";
            string[] symbols = new string[] { "USD", "GBP" };

            //act
            Client.GetLatestRates(baseCurrency, symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/latest?base=ZAR&symbols=USD,GBP", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetLatestRatesAsync_Verify()
        {
            //arrange
            var handler = Initialize(null);

            //act
            await Client.GetLatestRatesAsync();

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/latest", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetLatestRatesAsync_WithBaseCurrency_Verify()
        {
            //arrange
            var handler = Initialize(null);
            string baseCurrency = "USD";

            //act
            await Client.GetLatestRatesAsync(baseCurrency);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/latest?base=USD", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetLatestRatesAsync_WithSymbols_Verify()
        {
            //arrange
            var handler = Initialize(null);
            string[] symbols = new string[] { "USD", "GBP" };

            //act
            await Client.GetLatestRatesAsync(symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/latest?symbols=USD,GBP", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetLatestRatesAsync_WithBaseCurrencyAndSymbols_Verify()
        {
            //arrange
            var handler = Initialize(() => exchangeRatesHttpResult);
            string baseCurrency = "ZAR";
            string[] symbols = new string[] { "USD", "GBP" };
            var client = Client;

            //act
            ExchangeRates actualResult = await Client.GetLatestRatesAsync(baseCurrency, symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/latest?base=ZAR&symbols=USD,GBP", handler.Requests[0].RequestUri.AbsoluteUri);
            Assert.AreEqual("ZAR", actualResult.Base);
            Assert.AreEqual(new DateTime(2010, 1, 12), actualResult.Date);
            Assert.AreEqual(4, actualResult.Rates.Count);
            Assert.IsTrue(actualResult.Rates.ContainsKey("EUR"));
            Assert.AreEqual(0.0923668071D, actualResult.Rates["EUR"]);
            Assert.IsTrue(actualResult.Rates.ContainsKey("JPY"));
            Assert.AreEqual(12.2302889234D, actualResult.Rates["JPY"]);
            Assert.IsTrue(actualResult.Rates.ContainsKey("USD"));
            Assert.AreEqual(0.1337563733D, actualResult.Rates["USD"]);
            Assert.IsTrue(actualResult.Rates.ContainsKey("GBP"));
            Assert.AreEqual(0.0828714993D, actualResult.Rates["GBP"]);
        }


        [TestMethod]
        public void GetRatesForDate_Verify()
        {
            //arrange
            var handler = Initialize(null);
            DateTime dateTime = new DateTime(2010, 1, 12);

            //act
            Client.GetRatesForDate(dateTime);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/2010-01-12", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public void GetRatesForDate_WithBaseCurrency_Verify()
        {
            //arrange
            var handler = Initialize(null);
            DateTime dateTime = new DateTime(2010, 1, 12);
            string baseCurrency = "USD";

            //act
            Client.GetRatesForDate(dateTime, baseCurrency);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/2010-01-12?base=USD", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public void GetRatesForDate_WithSymbols_Verify()
        {
            //arrange
            var handler = Initialize(null);
            DateTime dateTime = new DateTime(2010, 1, 12);
            string[] symbols = new string[] { "USD", "GBP" };

            //act
            Client.GetRatesForDate(dateTime, symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/2010-01-12?symbols=USD,GBP", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public void GetRatesForDate_WithBaseCurrencyAndSymbols_Verify()
        {
            //arrange
            var handler = Initialize(null);
            DateTime dateTime = new DateTime(2010, 1, 12);
            string baseCurrency = "ZAR";
            string[] symbols = new string[] { "USD", "GBP" };

            //act
            Client.GetRatesForDate(dateTime, baseCurrency, symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/2010-01-12?base=ZAR&symbols=USD,GBP", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetRatesForDateAsync_Verify()
        {
            //arrange
            var handler = Initialize(null);
            DateTime dateTime = new DateTime(2010, 1, 12);

            //act
            await Client.GetRatesForDateAsync(dateTime);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/2010-01-12", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetRatesForDateAsync_WithBaseCurrency_Verify()
        {
            //arrange
            var handler = Initialize(null);
            DateTime dateTime = new DateTime(2010, 1, 12);
            string baseCurrency = "USD";

            //act
            await Client.GetRatesForDateAsync(dateTime, baseCurrency);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/2010-01-12?base=USD", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetRatesForDateAsync_WithSymbols_Verify()
        {
            //arrange
            var handler = Initialize(null);
            DateTime dateTime = new DateTime(2010, 1, 12);
            string[] symbols = new string[] { "USD", "GBP" };

            //act
            await Client.GetRatesForDateAsync(dateTime, symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/2010-01-12?symbols=USD,GBP", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetRatesForDateAsync_WithBaseCurrencyAndSymbols_Verify()
        {
            //arrange
            string httpResult = "{ \"rates\": { \"EUR\": 0.0923668071, \"JPY\": 12.2302889234, \"USD\": 0.1337563733, \"GBP\": 0.0828714993 }, \"base\": \"ZAR\", \"date\": \"2010-01-12\" }";
            var handler = Initialize(() => httpResult);
            DateTime dateTime = new DateTime(2010, 1, 12);
            string baseCurrency = "ZAR";
            string[] symbols = new string[] { "USD", "GBP" };
            var client = Client;

            //act
            ExchangeRates actualResult = await Client.GetRatesForDateAsync(dateTime, baseCurrency, symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/2010-01-12?base=ZAR&symbols=USD,GBP", handler.Requests[0].RequestUri.AbsoluteUri);
            Assert.AreEqual("ZAR", actualResult.Base);
            Assert.AreEqual(dateTime, actualResult.Date);
            Assert.AreEqual(4, actualResult.Rates.Count);
            Assert.IsTrue(actualResult.Rates.ContainsKey("EUR"));
            Assert.AreEqual(0.0923668071D, actualResult.Rates["EUR"]);
            Assert.IsTrue(actualResult.Rates.ContainsKey("JPY"));
            Assert.AreEqual(12.2302889234D, actualResult.Rates["JPY"]);
            Assert.IsTrue(actualResult.Rates.ContainsKey("USD"));
            Assert.AreEqual(0.1337563733D, actualResult.Rates["USD"]);
            Assert.IsTrue(actualResult.Rates.ContainsKey("GBP"));
            Assert.AreEqual(0.0828714993D, actualResult.Rates["GBP"]);
        }

        [TestMethod]
        public void GetRatesForDateRange_Verify()
        {
            //arrange
            var handler = Initialize(() => exchangeRatesRangeHttpResult);
            DateTime fromDate = new DateTime(2018, 1, 1);
            DateTime toDate = new DateTime(2018, 1, 3);
            var client = Client;

            //act
            ExchangeRatesRange actualResult = Client.GetRatesForDateRange(fromDate, toDate);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/history?start_at=2017-12-31&end_at=2018-01-03", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public void GetRatesForDateRange_WithBaseCurrency_Verify()
        {
            //arrange
            var handler = Initialize(() => exchangeRatesRangeHttpResult);
            DateTime fromDate = new DateTime(2018, 1, 1);
            DateTime toDate = new DateTime(2018, 1, 3);
            string baseCurrency = "ZAR";
            var client = Client;

            //act
            ExchangeRatesRange actualResult = Client.GetRatesForDateRange(fromDate, toDate, baseCurrency);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/history?start_at=2017-12-31&end_at=2018-01-03&base=ZAR", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public void GetRatesForDateRange_WithSymbols_Verify()
        {
            //arrange
            var handler = Initialize(() => exchangeRatesRangeHttpResult);
            DateTime fromDate = new DateTime(2018, 1, 1);
            DateTime toDate = new DateTime(2018, 1, 3);
            string[] symbols = new string[] { "ILS", "JPY" };
            var client = Client;

            //act
            ExchangeRatesRange actualResult = Client.GetRatesForDateRange(fromDate, toDate, symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/history?start_at=2017-12-31&end_at=2018-01-03&symbols=ILS,JPY", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public void GetRatesForDateRange_WithBaseCurrencyAndSymbols_Verify()
        {
            //arrange
            var handler = Initialize(() => exchangeRatesRangeHttpResult);
            DateTime fromDate = new DateTime(2018, 1, 1);
            DateTime toDate = new DateTime(2018, 1, 3);
            string baseCurrency = "ZAR";
            string[] symbols = new string[] { "ILS", "JPY" };
            var client = Client;

            //act
            ExchangeRatesRange actualResult = Client.GetRatesForDateRange(fromDate, toDate, baseCurrency, symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/history?start_at=2017-12-31&end_at=2018-01-03&base=ZAR&symbols=ILS,JPY", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetRatesForDateRangeAsync_Verify()
        {
            //arrange
            var handler = Initialize(() => exchangeRatesRangeHttpResult);
            DateTime fromDate = new DateTime(2018, 1, 1);
            DateTime toDate = new DateTime(2018, 1, 3);
            var client = Client;

            //act
            ExchangeRatesRange actualResult = await Client.GetRatesForDateRangeAsync(fromDate, toDate);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/history?start_at=2017-12-31&end_at=2018-01-03", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetRatesForDateRangeAsync_WithBaseCurrency_Verify()
        {
            //arrange
            var handler = Initialize(() => exchangeRatesRangeHttpResult);
            DateTime fromDate = new DateTime(2018, 1, 1);
            DateTime toDate = new DateTime(2018, 1, 3);
            string baseCurrency = "ZAR";
            var client = Client;

            //act
            ExchangeRatesRange actualResult = await Client.GetRatesForDateRangeAsync(fromDate, toDate, baseCurrency);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/history?start_at=2017-12-31&end_at=2018-01-03&base=ZAR", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetRatesForDateRangeAsync_WithSymbols_Verify()
        {
            //arrange
            var handler = Initialize(() => exchangeRatesRangeHttpResult);
            DateTime fromDate = new DateTime(2018, 1, 1);
            DateTime toDate = new DateTime(2018, 1, 3);
            string[] symbols = new string[] { "ILS", "JPY" };
            var client = Client;

            //act
            ExchangeRatesRange actualResult = await Client.GetRatesForDateRangeAsync(fromDate, toDate, symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/history?start_at=2017-12-31&end_at=2018-01-03&symbols=ILS,JPY", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetRatesForDateRangeAsync_WithBaseCurrencyAndSymbols_Verify()
        {
            //arrange
            var handler = Initialize(() => exchangeRatesRangeHttpResult);
            DateTime fromDate = new DateTime(2018, 1, 1);
            DateTime toDate = new DateTime(2018, 1, 3);
            string baseCurrency = "ZAR";
            string[] symbols = new string[] { "ILS", "JPY" };
            var client = Client;

            //act
            ExchangeRatesRange actualResult = await Client.GetRatesForDateRangeAsync(fromDate, toDate, baseCurrency, symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/history?start_at=2017-12-31&end_at=2018-01-03&base=ZAR&symbols=ILS,JPY", handler.Requests[0].RequestUri.AbsoluteUri);
            Assert.AreEqual(baseCurrency, actualResult.Base);
            Assert.AreEqual(fromDate.AddDays(-1), actualResult.StartAt);
            Assert.AreEqual(toDate, actualResult.EndAt);
            Assert.AreEqual(2, actualResult.RateRanges.Count);
            Assert.IsTrue(actualResult.RateRanges.ContainsKey(toDate));
            Assert.IsTrue(actualResult.RateRanges.ContainsKey(toDate.AddDays(-1)));
            Assert.AreEqual(2, actualResult.RateRanges[toDate].Count);
            Assert.AreEqual(9.0678222312D, actualResult.RateRanges[toDate]["JPY"]);
        }
    }

    class TestHttpMessageHandler : HttpMessageHandler
    {
        public List<HttpRequestMessage> Requests = new List<HttpRequestMessage>();

        public TestHttpMessageHandler(Func<string> resultFunc)
        {
            MessageResponseFunc = resultFunc;
        }

        public Func<string> MessageResponseFunc { get; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Requests.Add(request);
            string resultText = MessageResponseFunc?.Invoke() ?? string.Empty;
            return Task.FromResult(new HttpResponseMessage() { Content = new StringContent(resultText) });
        }
    }
}
