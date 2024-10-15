using Domain.Primitives;
using Domain.Shared;
using System.Security.Claims;

namespace Domain.Entities
{
    public sealed class Author : Entity
    {
        public Guid UserId { get; private set; }
        public User? User { get; private set; }
        public string UrlPage { get; private set; }
        public string? Description { get; private set; }

        public Author(Guid userId, string urlPage) 
            : base(userId) {
            UserId = userId;
            UrlPage = urlPage;
        }

        public Result<ClaimsIdentity> AuthenticateAuthor(ClaimsIdentity claimsIdentity) {
            claimsIdentity.AddClaim(new Claim("UrlPage", UrlPage));

            return Result.Success(claimsIdentity);
        }

        public Result ChangeSettings(string description) {
            if (string.IsNullOrWhiteSpace(description)) return Result.Failure();

            Description = description.Trim();

            return Result.Success();
        }
    }
}
