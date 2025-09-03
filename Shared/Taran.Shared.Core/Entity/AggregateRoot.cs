using Taran.Shared.Core.Primitives;

namespace Taran.Shared.Core.Entity
{
    public abstract class AggregateRoot<PrimaryKey> : BaseEntity<PrimaryKey>
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public AggregateRoot(int creatorUserId) : base(creatorUserId) { }

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.ToList();

        public void ClearDomainEvents() => _domainEvents.Clear();

        protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
            _domainEvents.Add(domainEvent);
    }
}