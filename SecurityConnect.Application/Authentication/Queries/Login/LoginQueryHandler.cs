using ErrorOr;
using MediatR;
using SecurityConnect.Application.Common.Interfaces.Authentication;
using SecurityConnect.Application.Common.Interfaces.Persistence;
using SecurityConnect.Domain.Entities;
using SecurityConnect.Domain.Common.Errors;
using SecurityConnect.Application.Authentication.Common;

namespace SecurityConnect.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler :
        IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {

        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public LoginQueryHandler(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }



        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // 1. Validate the user exists
            if (_userRepository.GetUserByUserName(query.UserName) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // 2. Validate the password is correct
            if (user.Password != query.Password)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // 3. Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token
                );

        }
    }
}
