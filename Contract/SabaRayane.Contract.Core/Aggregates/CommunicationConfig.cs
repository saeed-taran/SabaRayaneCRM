using Taran.Shared.Core.Entity;
using Taran.Shared.Core.Exceptions;

namespace SabaRayane.Contract.Core.Aggregates;

public class CommunicationConfig : AggregateRoot<int>
{
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }

    public CommunicationConfig(int creatorUserId, TimeOnly startTime, TimeOnly endTime) : base(creatorUserId)
    {
        if (startTime >= endTime)
            throw new DomainInvalidArgumentException(nameof(StartTime));

        StartTime = startTime;
        EndTime = endTime;
    }

    public void Update(int userId, TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime >= endTime)
            throw new DomainInvalidArgumentException(nameof(StartTime));

        StartTime = startTime;
        EndTime = endTime;

        Update(userId);
    }
}
