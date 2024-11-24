using System.ComponentModel.DataAnnotations;

namespace MonkeyMoneyApp.Models
{
    public class Login
    {
        [Required(ErrorMessage ="O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email invalido")]
        public string? Email { get; set; }
        [Required(ErrorMessage ="A senha é obrigatória")]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }
        [Display(Name = "Lembrar-me")]
        public bool GuardarSenha { get; set; }
    }
}
