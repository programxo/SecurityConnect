using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using SecurityConnect.Application.Authentication.Commands.Register;
using SecurityConnect.Application.Authentication.Common;
using SecurityConnect.Application.Authentication.Queries.Login;
using SecurityConnect.Contracts.Authentication;

namespace SecurityConnect.API.Controllers
{
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController : APIController
    {

        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // REPLACED BY MEDIATR
        //private readonly IAuthenticationCommandService _authenticationCommandService;
        //private readonly IAuthenticationQueryService _authenticationQueryService;

        //public AuthenticationController(
        //    IAuthenticationCommandService authenticationCommandService, 
        //    IAuthenticationQueryService authenticationQueryService)
        //{
        //    _authenticationCommandService = authenticationCommandService;
        //    _authenticationQueryService = authenticationQueryService;
        //}

        #region REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            // Replaced by Object Mapper (Mapster)
            // new RegisterCommand(request.PersonnelNumber, request.Password, request.FirstName, request.LastName);

            ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

            // ALSO REPLACED BY MEDIATR
            //_authenticationCommandService.Register(
            //request.PersonnelNumber,
            //request.Password,
            //request.FirstName,
            //request.LastName);

            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)), // MapAuthResult replaced by Mapster
                errors => Problem(errors));
        }
        #endregion

        #region LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);
            // Replaced by Object Mapper (Mapster)
            // new LoginQuery(request.PersonnelNumber, request.Password);
            var authResult = await _mediator.Send(query);

            //_authenticationQueryService.Login(request.PersonnelNumber,
            //                                          request.Password);

            //if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            //{
            //    return Problem(
            //        statusCode: StatusCodes.Status401Unauthorized, 
            //        title: authResult.FirstError.Description);
            //}

            return authResult.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors => Problem(errors));

        }
        #endregion


        //#region Mapping Authentication Result Method
        //private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        //{
        //    return new AuthenticationResponse(
        //        authResult.user.Id,
        //        authResult.user.PersonnelNumber,
        //        authResult.Token,
        //        authResult.user.FirstName,
        //        authResult.user.LastName);
        //}
        //#endregion
    }
}
