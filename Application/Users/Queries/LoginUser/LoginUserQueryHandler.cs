using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;
using System.Security.Claims;

namespace Application.Users.Queries.LoginUser
{
    public sealed class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, ClaimsIdentity>
    {
        private readonly IUserRepository userRepository;
        public LoginUserQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<Result<ClaimsIdentity>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(request.Email);

            return user.Authenticate();
        }
    }
}
