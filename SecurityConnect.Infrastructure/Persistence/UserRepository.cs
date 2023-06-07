using SecurityConnect.Application.Common.Interfaces.Persistence;
using SecurityConnect.Domain.Entities;

namespace SecurityConnect.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly static List<User> _users = new();
        public void Add(User user)
        {
            _users.Add(user);
        }

        public User? GetUserByUsername(string userName)
        {
            /* SingleOrDefault: UserName zu finden
               wenn kein passender User gibt null zurück,
               wirft eine InvalidOperationException, wenn mehr als ein passender User gefunden wird. */

            return _users.SingleOrDefault(u => u.Username == userName);
        }

    }
}
