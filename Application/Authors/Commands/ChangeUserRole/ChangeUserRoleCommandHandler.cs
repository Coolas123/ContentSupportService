using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;
using System.Security.Claims;

namespace Application.Authors.Commands.ChangeUserRole
{
    public sealed class ChangeUserRoleCommandHandler : ICommandHandler<ChangeUserRoleCommand, ClaimsIdentity>
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IUnitOfWork unitOfWork;

        public ChangeUserRoleCommandHandler(IAuthorRepository authorRepository,
            IUnitOfWork unitOfWork) {
            this.authorRepository = authorRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Result<ClaimsIdentity>> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken) {
            request.Author.User?.ChangeUserRole(request.UserRole);

            authorRepository.Update(request.Author);

            await unitOfWork.SaveChangesAsync();

            var claimsIdentity = request.Author.User?.Authenticate();

            return Result.Success(claimsIdentity.Value());
        }
    }
}
