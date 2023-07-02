namespace SecurityConnect.Domain.Entities.User;

public sealed class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; } // TODO: Hash this

    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public UserRole UserRole { get; private set; }

    public User(string firstName, string lastName, string username, string password, UserRole role)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        UserName = username;
        Password = password;
        UserRole = role;
    }

    public static User Create(string firstName, string lastName, string username, string password,
        UserRole role)
    {
        // TODO: enforce invariants
        return new User(
            firstName,
            lastName,
            username,
            password,
            role);
    }

#pragma warning disable CS8618
    private User()
    {
    }
#pragma warning restore CS8618
}
