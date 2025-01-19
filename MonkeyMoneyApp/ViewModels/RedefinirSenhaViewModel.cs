using System.ComponentModel.DataAnnotations;

public class RedefinirSenhaViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string NovaSenha { get; set; }
}
