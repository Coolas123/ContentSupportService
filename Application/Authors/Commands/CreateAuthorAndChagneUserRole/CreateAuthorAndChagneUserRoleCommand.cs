using Application.Abstractions.Messaging;
using Application.Authors.Commands.ChangeUserRole;
using Application.Authors.Commands.RegisterAuthor;
using Domain.Shared;
using System.Security.Claims;

namespace Application.Authors.Commands.CreateAuthorAndChagneUserRole
{
    public sealed class CreateAuthorAndChagneUserRoleCommand : ICommand<ClaimsIdentity>
    {
        public ChangeUserRoleCommand? ChangeUserRoleCommand { get; set; }
        public CreateAuthorCommand? CreateAuthorCommand { get; set; }
    }
}
