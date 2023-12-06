using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyExchange.API.Models
{
    public class Currency
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field must be fill")]
        [Length(3,3, ErrorMessage = "Code should be fixed length of 3")]
        public required string Code { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field must be fill")]
        public required string FullName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Field must be fill")]
        public required string Sign { get; set; }
    }
}
