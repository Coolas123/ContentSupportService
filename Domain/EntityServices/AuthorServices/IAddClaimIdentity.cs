using Domain.Shared;
using System.Security.Claims;

namespace Domain.EntityServices.AuthorServices
{
    public interface IAddClaimIdentity
    {
        Result<ClaimsIdentity> Add(ClaimsIdentity claimsIdentity, string claimName, string claimValue);
    }
}
