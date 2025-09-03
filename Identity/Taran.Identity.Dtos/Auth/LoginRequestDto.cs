using System.ComponentModel.DataAnnotations;

namespace Taran.Identity.Dtos.Auth;

public record LoginRequestDto
{
    [Required(ErrorMessage = "نام کاربری وارد نشده")]
    public string UserName { get; set; }
    
    [Required(ErrorMessage = "رمز عبور وارد نشده")]
    public string Password { get; set; }
}
