using System.ComponentModel.DataAnnotations;

namespace MyWebSite.Models;

public sealed class LoginViewModel
{
    [Required]
    [Display(Name = "Логин")]
    public string Login { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = "";
}
