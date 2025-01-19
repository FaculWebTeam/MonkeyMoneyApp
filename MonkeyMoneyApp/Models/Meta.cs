using System.ComponentModel.DataAnnotations;

namespace ApiMonkeyMoney.Models
{
    public class Meta
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 6, ErrorMessage = "O campo {0} precisa ter entre {1} e {2} caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage="O campo {0} é obrigatório")]
        public float ValorObjetivo { get; set; }
        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        public float ValorAtual { get; set; }
        public string UserId { get; set; }
    }
}
