using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.API.Contracts.ExchangeRate
{
    public class ExchangeRateRequest
    {
        [Required]
        public int BaseCurrencyId { get; set; }
        
        [Required]
        public int TargetCurrencyId { get; set; }
        
        [Required]
        [Range(1e-4, double.MaxValue)]
        public decimal Rate { get; set; }
    }
}
