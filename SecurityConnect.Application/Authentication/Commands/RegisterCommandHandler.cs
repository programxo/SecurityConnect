using ErrorOr;
using MediatR;
using SecurityConnect.Application.Common.Interfaces.Authentication;
using SecurityConnect.Application.Common.Interfaces.Persistence;
using SecurityConnect.Domain.Entities;
using SecurityConnect.Domain.Common.Errors;
using SecurityConnect.Application.Authentication.Common;

namespace SecurityConnect.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler :
        IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {

        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }



        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // 1. Validate the user doesn't exists
            if (_userRepository.GetUserByUserName(command.UserName) is not null)
            {
                return Errors.User.DuplicateUserName;
            }

            // 2. Create user (generate unique ID) & Persist to DB
            var user = new User
            {
                UserName = command.UserName,
                Password = command.Password,
                FirstName = command.FirstName,
                LastName = command.LastName
            };

            _userRepository.Add(user);

            // 3. Create JWT token

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token
                );

        }
    }
}
