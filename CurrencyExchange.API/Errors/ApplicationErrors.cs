using CurrencyExchange.API.Response;

namespace CurrencyExchange.API.Errors
{
    
    public static class ApplicationErrors
    {
        public static class CurrencyErrors
        {
            private static Error CreateError(string code, string? description = null) =>
                new Error("Currency." + code, description);
            public static Error AlreadyExists(string existedCurrencyCode) =>
                CreateError(nameof(AlreadyExists), 
                    $"The currency with the {existedCurrencyCode} code already exists");
            public static Error NotFound(string code) =>
                CreateError(nameof(NotFound),
                    $"The currency with the {code} code is not found");
            public static Error NotFound(int id) =>
                CreateError(nameof(NotFound),
                    $"The currency with id {id} is not found");

            public static Error InvalidLength() =>
                CreateError(nameof(InvalidLength), "The length of currency code must be 3");
        }

        public static class ExchangeRateErrors
        {
            private static Error CreateError(string code, string? description = null) =>
                new Error("ExchangeRate." + code, description);
            public static Error NotFound(string baseC, string targetC) =>
                CreateError(nameof(NotFound), $"The exchange rate for {baseC} to {targetC} is not found");
            public static Error NotFound(int id) =>
                CreateError(nameof(NotFound), $"The exchange rate with id {id} is not found");

            public static Error SameCurrencies() =>
                CreateError(nameof(SameCurrencies), "Currencies id must be different");

            public static Error NullValue(string fieldName) =>
                CreateError(nameof(NullValue), $"The field '{fieldName}' must be specified");
            
            public static Error InvalidLength(string? name = null) =>
                CreateError(nameof(InvalidLength), $"The length of the{(name is null ? "" : $" {name}")} currency code should be 3");
        }
        
        public static class ExchangeErrors
        {
             private static Error CreateError(string code, string? description = null) =>
                new Error("Exchange." + code, description);

             public static Error NegativeOrZeroAmount() =>
                 CreateError(nameof(NegativeOrZeroAmount), "Amount can't be negative or zero value");

             public static Error NotFound() =>
                 CreateError(nameof(NotFound), "The rate with specified code was not found");
        }
    }
}
