namespace SecurityConnect.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        User? Login(string username, string password); /* nullable (?) entweder ein User-Objekt oder null,
                                                     anstatt einen Fehler */
        void Register(User user); // sync: andere Operationen müssen warten

        Task<User> GetCurrentAdmin(string adminId);

        User? GetUser(string username);

        Task<bool> Delete(string Id);

        Task<List<User>> List(string adminId);
    }
}
