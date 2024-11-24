using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiMonkeyMoney.Models
{
    public class Transacao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "O campo {0} precisa ter entre {1} e {2} caracteres")]
        public string Titulo { get; set; }

        [StringLength(5000, ErrorMessage = "O campo {0} pode ter no máximo 5000 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public float Valor { get; set; }

        [ForeignKey("Banco")]
        public int? BancoId { get; set; }

        public Banco? Banco { get; set; }

        public DateTime Horario { get; set; }
    }
}
