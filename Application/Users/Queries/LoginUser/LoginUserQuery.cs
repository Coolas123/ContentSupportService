using Application.Abstractions.Messaging;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Application.Users.Queries.LoginUser
{
    public sealed class LoginUserQuery : IQuery<ClaimsIdentity>
    {
        [Display(Name = "Почта")]
        public string? Email { get; set; }
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
