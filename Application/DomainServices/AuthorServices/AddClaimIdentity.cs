using Domain.EntityServices.AuthorServices;
using Domain.Shared;
using System.Security.Claims;

namespace Application.DomainServices.AuthorServices
{
    public sealed class AddClaimIdentity: IAddClaimIdentity
    {
        public Result<ClaimsIdentity> Add(ClaimsIdentity claimsIdentity, string claimName, string claimValue) {
            claimsIdentity.AddClaim(new Claim(claimName, claimValue));

            return Result.Success(claimsIdentity);
        }
    }
}
