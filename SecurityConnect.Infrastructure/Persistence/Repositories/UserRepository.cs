using SecurityConnect.Application.Common.Interfaces.Persistence;

using Microsoft.EntityFrameworkCore;
using SecurityConnect.Domain.Entities.UserAggregate;

namespace SecurityConnect.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(User user)
        {
            _dbContext.Add(user);

            _dbContext.SaveChanges();
        }

        public User? GetUser(string username)
        {
            /* SingleOrDefault: UserName zu finden
               wenn kein passender User gibt null zurück,
               wirft eine InvalidOperationException, wenn mehr als ein passender User gefunden wird. */
            return _dbContext.Users
                .SingleOrDefault(u => u.UserName == username);
        }

        public async Task<IEnumerable<User?>> GetUsersAsync()
        {
            return await _dbContext.Users
                .ToListAsync();
        }

    }
}
