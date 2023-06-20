
using SecurityConnect.Domain.Entities.UserAggregate;

namespace SecurityConnect.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        User? GetUser(string username); /* nullable (?) entweder ein User-Objekt oder null,
                                             anstatt einen Fehler */

        Task<IEnumerable<User?>> GetUsersAsync();
        void Add(User user); // sync: andere Operationen müssen warten
    }
}
