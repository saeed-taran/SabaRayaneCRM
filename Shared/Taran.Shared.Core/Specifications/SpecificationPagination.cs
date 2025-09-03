using Taran.Shared.Core.Exceptions;

namespace Taran.Shared.Core.Specifications;

public record SpecificationPagination
{
    public SpecificationPagination(int skip, int take)
    {
        if(take <= 0)
            throw new DomainArgumentNullOrEmptyException(nameof(Take));
        if (skip < 0)
            throw new DomainArgumentNullOrEmptyException(nameof(Skip));

        Skip = skip;
        Take = take;
    }

    public int Skip { get; private set; }
    public int Take { get; private set; }
}
