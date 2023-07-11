namespace SecurityConnect.Domain.Common.Errors;
    public static partial class Errors
    {
        public static class Authentication
        {
            public static Error InvalidCredentials => Error.Validation(
                code: "Authentication.InvalidCredentials",
                description: "Invalid credentials."
                );

            public static Error InvalidRole => Error.Validation(
                code: "Authentication.InvalidRole",
                description: "Invalid user role."
                );
        }
    }

