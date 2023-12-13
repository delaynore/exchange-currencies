using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyExchange.API.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; }

        [Required]
        public int BaseCurrencyId { get; set; }
        public Currency BaseCurrency { get; set; }

        [Required]
        public int TargetCurrencyId { get; set; }
        public Currency TargetCurrency { get; set; }

        [Column(TypeName = "decimal(6)")]
        [Required]
        [Range(1e-4, double.MaxValue)]
        public decimal Rate {  get; set; }
    }
}
