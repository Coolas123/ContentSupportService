using Application.Abstractions.Messaging;
using Application.Authors.Commands.ChangeUserRole;
using Application.Authors.Queries.GetAuthor;
using Domain.Enums;
using Domain.Shared;
using MediatR;
using System.Security.Claims;

namespace Application.Authors.Commands.CreateAuthorAndChagneUserRole
{
    public sealed class CreateAuthorAndChagneUserRoleCommandHandler : ICommandHandler<CreateAuthorAndChagneUserRoleCommand, ClaimsIdentity>
    {
        private readonly ISender sender;

        public CreateAuthorAndChagneUserRoleCommandHandler(ISender sender) {
            this.sender = sender;
        }
        public async Task<Result<ClaimsIdentity>> Handle(CreateAuthorAndChagneUserRoleCommand request, CancellationToken cancellationToken) {
            await sender.Send(request?.CreateAuthorCommand);

            var author = await sender.Send(new GetAuthorQuery ((Guid)(request?.CreateAuthorCommand.UserId)));

            if (author.IsSuccess) {
                request.ChangeUserRoleCommand = new ChangeUserRoleCommand
                {
                    Author = author.Value(),
                    UserRole = UserRole.Author
                };
            }

            var claimsIdentity = await sender.Send(request.ChangeUserRoleCommand);
            return Result.Success(claimsIdentity.Value());
        }
    }
}
