using Domain.DomainEvents;
using MediatR;

namespace Application.Users.Events
{
    public sealed class PatronCreatedDomainEventHandler
    {
        public Task Handle(PatronCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
