using System.Security;

namespace SecurityConnect.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserName { get; set; } = null!;   /* null! Null-Forgiving-Postfix
                                                        Zeigt dem Compiler, dass eine Variable oder Eigenschaft,
                                                        die null sein könnte in Wirklichkeit nicht null ist */
        public SecureString Password { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

    }
}
