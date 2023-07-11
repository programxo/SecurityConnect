using MediatR;
using SecurityConnect.Application.Admin.Queries;

namespace SecurityConnect.Application.Authentication.Commands.Register.Employee
{
    public class RegisterEmployeeCommandHandler :
        IRequestHandler<RegisterEmployeeCommand, ErrorOr<AuthenticationResult>>
    {

        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;

        public RegisterEmployeeCommandHandler(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository,
            IMediator mediator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterEmployeeCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            // 1. Validate the user doesn't exist
            if (_userRepository.GetUser(command.UserName) is not null)
            {
                return Errors.User.DuplicateUserName;
            }

            // 2. Get the current admin
            var currentAdmin = await _mediator.Send(new GetCurrentAdminQuery (command.ManagedByAdminId ));

            if (currentAdmin == null)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            // 3. Create user (generate unique ID) & Persist to DB
            var user = User.CreateEmployee(command.FirstName, command.LastName, command.UserName, command.Password, currentAdmin);

            _userRepository.Register(user);

            // 4. Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
