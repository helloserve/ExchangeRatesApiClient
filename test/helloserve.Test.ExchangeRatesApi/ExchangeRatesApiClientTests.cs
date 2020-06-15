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
            var client = Client;

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
            var client = Client;

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
            var client = Client;

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
            var client = Client;

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
            var client = Client;

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
            var client = Client;

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
            var client = Client;

            //act
            await Client.GetLatestRatesAsync(symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/latest?symbols=USD,GBP", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetLatestRatesAsync_WithBaseCurrencyAndSymbols_Verify()
        {
            //arrange
            string httpResult = "{ \"rates\": { \"EUR\": 0.0923668071, \"JPY\": 12.2302889234, \"USD\": 0.1337563733, \"GBP\": 0.0828714993 }, \"base\": \"ZAR\", \"date\": \"2010-01-12\" }";
            var handler = Initialize(() => httpResult);
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
            var client = Client;

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
            var client = Client;

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
            var client = Client;

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
            var client = Client;

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
            var client = Client;

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
            var client = Client;

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
            var client = Client;

            //act
            await Client.GetRatesForDateAsync(dateTime, symbols);

            //assert
            Assert.AreEqual("https://api.exchangeratesapi.io/2010-01-12?symbols=USD,GBP", handler.Requests[0].RequestUri.AbsoluteUri);
        }

        [TestMethod]
        public async Task GetRatedForDateAsync_WithBaseCurrencyAndSymbols_Verify()
        {
            //arrange
            string httpResult = "{ \"rates\": { \"EUR\": 0.0923668071, \"JPY\": 12.2302889234, \"USD\": 0.1337563733, \"GBP\": 0.0828714993 }, \"base\": \"ZAR\", \"date\": \"2010-01-12\" }";
            DateTime dateTime = new DateTime(2010, 1, 12);
            var handler = Initialize(() => httpResult);
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
