namespace SecurityConnect.Application.Admin.Queries
{
    public record GetCurrentAdminQuery
    (
        string AdminId

    ) : IRequest<User>;

}
