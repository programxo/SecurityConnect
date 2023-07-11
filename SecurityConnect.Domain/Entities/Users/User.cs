using System.Text.Json.Serialization;

namespace SecurityConnect.Domain.Entities.Users
{
    public sealed class User
    {
        #region Constants
        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        #endregion

        #region Properties
        public UserRole UserRole { get; private set; }
        #endregion

        #region Navigation Properties
        public string ManagedByAdminId { get; private set; }

        [JsonIgnore]
        public User ManagedByAdmin { get; private set; }
        #endregion

        #region Methods
        public User(string firstName, string lastName, string username, string password, UserRole role, User managedByAdmin)
        {
            Id = Guid.NewGuid()
                .ToString(); // Als string speichern
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
            Password = BCrypt.Net.BCrypt.HashPassword(password);
            UserRole = role;
            ManagedByAdmin = managedByAdmin;
            ManagedByAdminId = managedByAdmin?.Id ?? string.Empty; // Als string setzen
        }
        #endregion

        #region Static Methods
        public static User CreateAdmin(string firstName, string lastName, string username, string password)
        {
            var admin = new User(firstName, lastName, username, password, UserRole.Admin, null);
            admin.SetManagedByAdmin(admin); // Setzt die Beziehung zum ersten Admin-Benutzer selbst

            return admin;
        }
        #endregion

        #region Beziehung setzen
        public void SetManagedByAdmin(User admin)
        {
            if (UserRole == UserRole.Admin)
            {
                ManagedByAdmin = admin;
                ManagedByAdminId = admin?.Id.ToString() ?? string.Empty;
            }
        }
        #endregion

        #region Employee erstellen
        public static User CreateEmployee(string firstName, string lastName, string username, string password, User managedByAdmin)
        {
            return new User(firstName, lastName, username, password, UserRole.Employee, managedByAdmin);
        }
        #endregion

        #region Password Method
        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
        #endregion

        #region Parameterless constructor for EF
        protected User()
        {

        }
        #endregion
    }
}
