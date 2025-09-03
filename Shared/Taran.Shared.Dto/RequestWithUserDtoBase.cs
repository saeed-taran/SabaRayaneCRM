namespace Taran.Shared.Dtos;

public record RequestWithUserDtoBase
{
    private int UserId;

    public void SetUserId(int userId)
    {
        UserId = userId;
    }

    public int GetUserId()
    {
        return UserId;
    }
}