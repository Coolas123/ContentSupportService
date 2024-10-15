using Domain.DomainEvents;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Events
{
    internal sealed class ClaimIdentitiesChangedDomainEventHandler : INotificationHandler<ClaimIdentitiesChangedDomainEvent>
    {
        private readonly IUserRepository userRepository;

        public ClaimIdentitiesChangedDomainEventHandler(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public async Task Handle(ClaimIdentitiesChangedDomainEvent notification, CancellationToken cancellationToken) {
            User user = await userRepository.GetByIdAsync(notification.UserId);

            if (user == null) {
                return;
            }

            
        }
    }
}
