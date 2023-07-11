namespace SecurityConnect.Application.Admin.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly IUserRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(IUserRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            
            return await _employeeRepository.Delete(command.Id);
        }
    }

}
