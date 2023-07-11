namespace SecurityConnect.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Register(User user)
        {
            _dbContext.Add(user);
            _dbContext.SaveChanges();
        }

        public User? Login(string username, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.UserName == username);
            if (user != null && user.VerifyPassword(password))
            {
                return user;
            }
            return null;
        }

        public async Task<bool> Delete(string id)
        {
            var toDelete = await _dbContext.Users.FindAsync(id);

            if (toDelete == null) return false;

            _dbContext.Users.Remove(toDelete);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<User>> List(string managedByAdminId)
        {
            return await _dbContext.Users
                .Where(e => e.ManagedByAdminId == managedByAdminId)
                .ToListAsync();
        }

        public async Task<User> GetCurrentAdmin(string userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId && u.UserRole == UserRole.Admin);
        }

        public User? GetUser(string username)
        {
            return _dbContext.Users.SingleOrDefault(u => u.UserName == username);
        }
    }
}
