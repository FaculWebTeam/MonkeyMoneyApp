using System.ComponentModel.DataAnnotations;

public class ResetPassword
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
    public string ConfirmPassword { get; set; }
}
