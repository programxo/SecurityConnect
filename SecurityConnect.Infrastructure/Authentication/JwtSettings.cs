namespace SecurityConnect.Infrastructure.Authentication
{
    public class JwtSettings /* Settings-DTO for JWT */
    {
        public const string SectionName = "JwtSettings";
        public string Secret { get; init; } /* init : Diese Eigenschaft kann nur während der Initialisierung
                                               gesetzt werden und wird es unveränderlich */
        public int ExpiryMinutes { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }

    }
}
