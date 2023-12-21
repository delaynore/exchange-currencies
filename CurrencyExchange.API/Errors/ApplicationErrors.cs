using CurrencyExchange.API.Response;

namespace CurrencyExchange.API.Errors
{
    public static class ApplicationErrors
    {
        public static class CurrencyErrors
        {
            public static Error AlreadyExists =>
                new("Currency.AlreadyExists", 
                    "Currency with the same code already exists");
            public static Error NotFound =>
                new("Currency.NotFound",
                    "Currency is not found");
        }

        public static class ExchangeRateErrors
        {
            public static Error NotFound =>
                new("ExchangeRate.NotFound", "Exchange rate is not found");

            public static Error SameCurrencies =>
                new("ExchangeRate.SameCurrencies", "Currencies id must be different");
        }
    }
}
