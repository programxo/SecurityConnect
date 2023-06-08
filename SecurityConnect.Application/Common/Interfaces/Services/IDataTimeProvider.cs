namespace SecurityConnect.Application.Common.Interfaces.Services
{
    public interface IDateTimeProvider /* UTC Now Time from System Provider */
    {
        DateTime UtcNow { get; }
    }
}
