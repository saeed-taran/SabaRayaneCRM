namespace Taran.Shared.Dtos;

public record PaginatedResponseDto<Result>
{
    public int Skip { get; private set; }
    public int Take { get; private set; }
    public int TotalCount { get; private set; }
    public ICollection<Result> Results { get; private set; }

    public PaginatedResponseDto(int skip, int take, int totalCount, ICollection<Result> results)
    {
        Skip = skip;
        Take = take;
        TotalCount = totalCount;
        Results = results;
    }
}