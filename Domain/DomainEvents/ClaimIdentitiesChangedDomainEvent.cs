using Domain.Primitives;

namespace Domain.DomainEvents
{
    public sealed class ClaimIdentitiesChangedDomainEvent : IDomainEvent
    {
        public Guid UserId { get; set; }
    }
}
