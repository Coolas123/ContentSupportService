using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Shared;
using System.Security.Claims;

namespace Application.Authors.Commands.RegisterAuthor
{
    public sealed class CreateAuthorCommand : ICommand<Author>
    {
        public Guid UserId { get; set; }
        public string? UrlPage { get; set; }
    }
}
