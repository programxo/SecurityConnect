using SecurityConnect.Application.Common.Interfaces.Services;

namespace SecurityConnect.Infrastructure.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
