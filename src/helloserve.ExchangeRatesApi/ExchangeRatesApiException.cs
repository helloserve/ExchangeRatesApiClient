using System;

namespace helloserve.ExchangeRatesApi
{
    public class ExchangeRatesApiException : Exception
    {
        public ExchangeRatesApiErrorCode ErrorCode { get; set; }

        public ExchangeRatesApiException(ExchangeRatesApiErrorCode errorCode, string info) : base(info)
        {
            ErrorCode = errorCode;
        }

        internal static ExchangeRatesApiException CreateFromResponse(Models.ApiResponseError error)
        {
            if (int.TryParse(error.Code, out int codeValue))
            {
                return new ExchangeRatesApiException((ExchangeRatesApiErrorCode)codeValue, error.Info);
            }

            return new ExchangeRatesApiException(ExchangeRatesApiErrorCode.Unknown, $"Unknown error occured: {error.Code} {error.Info}");
        }
    }
}
