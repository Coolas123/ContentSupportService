using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Enums;
using Domain.Shared;
using System.Security.Claims;

namespace Application.Authors.Commands.ChangeUserRole
{
    public sealed class ChangeUserRoleCommand : ICommand<ClaimsIdentity>
    {
        public Author Author { get; set; }
        public UserRole UserRole { get; set; }
    }
}
