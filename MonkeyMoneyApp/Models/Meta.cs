using System.ComponentModel.DataAnnotations;

namespace ApiMonkeyMoney.Models
{
    public class Meta
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage="O campo {0} é obrigatório")]
        public float ValorObjetivo { get; set; }
        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        public float ValorAtual { get; set; }
        public float Deposito { get; set; } //Vai atualizando o valor atual, esse campo não deve aparecer para o usuário, somente se ele for adicionar um novo valor 
    }
}
