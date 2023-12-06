using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyExchange.API.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; }

        public int BaseCurrencyId { get; set; }
        public Currency BaseCurrency { get; set; }

        public int TargetCurrencyId { get; set; }
        public Currency TargetCurrency { get; set; }

        [Column(TypeName = "decimal(6)")]
        public decimal Rate {  get; set; }
    }
}
