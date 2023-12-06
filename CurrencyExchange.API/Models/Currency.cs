namespace CurrencyExchange.API.Models
{
    public class Currency
    {
        
        public int Id { get; set; }
        public required string Code { get; set; }
        public required string FullName { get; set; }
        public required string Sign { get; set; }

        public ICollection<ExchangeRate> ExchangeRates { get; set; } = new List<ExchangeRate>();
    }
}
