namespace SecurityConnect.Application.Admin.Employee.Queries.ListEmployees
{
    public class ListEmployeesQueryHandler : IRequestHandler<ListEmployeesQuery, List<User>>
    {
        private readonly IUserRepository _userRepository;

        public ListEmployeesQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> Handle(ListEmployeesQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.List(request.AdminId);
            return users.Where(u => u.UserRole == UserRole.Employee).ToList();
        }
    }
}
