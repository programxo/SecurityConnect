namespace SecurityConnect.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        #region Member Variables
        private readonly JwtSettings _jwtSettings;

        private readonly IDateTimeProvider _dateTimeProvider;
        #endregion

        #region Constructor for Dependency Injection
        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtOptions.Value;
        }
        #endregion

        #region Method: Generating JWTs
        public string GenerateToken(User user)
        {
            #region Creating Signature Information for Token
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);
            #endregion

            #region Creating Claims for Token (Details about User)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Typ, user.UserRole.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.ManagedByAdminId)
            };
            #endregion

            #region Creating Token

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials);
            #endregion

            // Converting Token into String with WriteToken Method and returning it.
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
        #endregion
    }
}
