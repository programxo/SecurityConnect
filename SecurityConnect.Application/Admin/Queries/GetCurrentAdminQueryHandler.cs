namespace SecurityConnect.Application.Admin.Queries
{
    public class GetCurrentAdminQueryHandler : IRequestHandler<GetCurrentAdminQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public GetCurrentAdminQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetCurrentAdminQuery query, CancellationToken cancellationToken)
        {
            return await _userRepository.GetCurrentAdmin(query.AdminId);
        }
    }
}
