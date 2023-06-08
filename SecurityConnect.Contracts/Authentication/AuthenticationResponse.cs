namespace SecurityConnect.Contracts.Authentication
{
    /* AuthenticationResponse is a DTO used for providing authentication response. 
       It encapsulates the user's id, username, token, first name, and last name. */

    /* `AuthenticationResponse` is defined as a record for immutability and value semantics.
        Immutability is important because once a response is created, it should not change.
        Value semantics allow us to consider two responses with the same values as equal,
        which can be beneficial in certain scenarios. */

    public record AuthenticationResponse(
        Guid Id,
        string UserName,
        string Token,
        string FirstName,
        string LastName
    );

}
