namespace Taran.Identity.Dtos.Auth;

public record LoginResponseDto
{
    public string Token { get; set; }

    public LoginResponseDto(string token)
    {
        Token = token;
    }
}
