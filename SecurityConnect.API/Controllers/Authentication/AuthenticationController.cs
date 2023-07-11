using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using SecurityConnect.Application.Authentication.Queries.Login;
using SecurityConnect.Contracts.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace SecurityConnect.WebApp.Controllers.Authentication
{
    [Route("auth")]
    [AllowAnonymous]
    public class AuthenticationController : APIController
    {

        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        private readonly AuthenticationService _authService;  // Injizieren Sie den AuthenticationService

        public AuthenticationController(ISender mediator, IMapper mapper, AuthenticationService authService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _authService = authService;  // Setzen Sie den AuthenticationService
        }

        #region LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);

            var authResult = await _mediator.Send(query);

            return authResult.Match(
                authResult => {
                    var response = _mapper.Map<AuthenticationResponse>(authResult);
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(response.Token);

                    var usernameClaim = jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Name);
                    var roleClaim = jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Typ);

                    _authService.Login(usernameClaim.Value, roleClaim.Value);
                    return Ok(response);
                },
                errors => Problem(errors));
        }
        #endregion
    }
}
