using System.ComponentModel.DataAnnotations;

namespace ApiMonkeyMoney.Models
{
    public class Banco
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [StringLength(100, MinimumLength = 6, ErrorMessage ="O campo {0} precisa ter entre {1} e {2} caracteres")]
        public string Nome { get; set; }
        public string UserId { get; set; }
    }
}
