using SecurityConnect.Domain.Common.Models;
using SecurityConnect.Domain.Entities.UserAggregate.ValueObjects;

namespace SecurityConnect.Domain.Entities.UserAggregate;

public sealed class User : AggregateRoot<UserId, Guid>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string UserName { get; private set; }
    public string Password { get; private set; } // TODO: Hash this

    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    private User(string firstName, string lastName, string username, string password, UserId? userId = null)
        : base(userId ?? UserId.CreateUnique())
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = username;
        Password = password;
    }

    public static User Create(string firstName, string lastName, string username, string password)
    {
        // TODO: enforce invariants
        return new User(
            firstName,
            lastName,
            username,
            password);
    }

#pragma warning disable CS8618
    private User()
    {
    }
#pragma warning restore CS8618
}
