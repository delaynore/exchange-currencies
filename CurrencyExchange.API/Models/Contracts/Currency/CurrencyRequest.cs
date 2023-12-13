using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.API.Models.Contracts.Currency
{
    public class CurrencyRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field must be fill")]
        [Length(3, 3, ErrorMessage = "Code should be fixed length of 3")]
        public required string Code { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field must be fill")]
        public required string FullName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Field must be fill")]
        public required string Sign { get; set; }
    }
}
