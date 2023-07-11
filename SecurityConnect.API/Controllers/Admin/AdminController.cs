using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityConnect.Application.Admin.Employee.Commands.DeleteEmployee;
using SecurityConnect.Application.Admin.Employee.Queries.ListEmployees;
using SecurityConnect.Application.Admin.Queries;
using SecurityConnect.Application.Authentication.Commands.Register;
using SecurityConnect.Application.Authentication.Commands.Register.Employee;
using SecurityConnect.Contracts.Admin.Employee;
using SecurityConnect.Contracts.Authentication;
using System.Security.Claims;

namespace SecurityConnect.WebApp.Controllers.Admin
{
    [Route("admin")]
#if DEBUG
    [AllowAnonymous]
#endif
    public class AdminController : APIController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AdminController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("registeradmin")]
        public async Task<IActionResult> RegisterAdmin(RegisterAdminRequest request)
        {
            if (request.UserRole != "Admin")
            {
                return BadRequest("User role must be Admin to register as an admin");
            }

            var command = new RegisterAdminCommand(request.FirstName, request.LastName, request.UserName, request.Password);

            var authResult = await _mediator.Send(command);

            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));
        }

        [HttpPost("registeremployee")]
        public async Task<IActionResult> RegisterEmployee(RegisterEmployeeRequest request)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentAdmin = await _mediator.Send(new GetCurrentAdminQuery(adminId));

            if (currentAdmin == null)
            {
                return BadRequest("Invalid admin");
            }

            var command = new RegisterEmployeeCommand(request.FirstName, request.LastName, request.UserName, request.Password, currentAdmin.Id);

            var authResult = await _mediator.Send(command);

            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));
        }

        [HttpGet("listemployees")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<EmployeeResponse>>> GetEmployees()
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var currentAdmin = await _mediator.Send(new GetCurrentAdminQuery(adminId));

            if (currentAdmin == null)
            {
                return BadRequest("Invalid admin");
            }

            var query = new ListEmployeesQuery(adminId);
            var employees = await _mediator.Send(query);

            return Ok(employees);
        }

        [HttpDelete("deleteemployee/{id}")]
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(id));
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
