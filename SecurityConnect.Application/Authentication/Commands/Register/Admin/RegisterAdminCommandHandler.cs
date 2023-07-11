using SecurityConnect.Application.Authentication.Commands.Register;

public class RegisterAdminCommandHandler : IRequestHandler<RegisterAdminCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterAdminCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterAdminCommand command, CancellationToken cancellationToken)
    {
        // 1. Validate the user doesn't exist
        if (_userRepository.GetUser(command.UserName) != null)
        {
            return Errors.User.DuplicateUserName;
        }

        // 2. Create admin user & persist to DB
        var admin = User.CreateAdmin(command.FirstName, command.LastName, command.UserName, command.Password);

        _userRepository.Register(admin);

        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(admin);

        return new AuthenticationResult(admin, token);
    }
}
