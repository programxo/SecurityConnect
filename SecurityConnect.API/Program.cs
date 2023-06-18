using SecurityConnect.WebApp;
using SecurityConnect.Application;
using SecurityConnect.Infrastructure;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();

    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Services.AddMudServices();
}

var app = builder.Build();
{
    // Configuring global exception handler
    app.UseExceptionHandler("/error");

    app.UseStaticFiles();
    app.UseRouting();

    // Configure the HTTP request pipeline.
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapBlazorHub();
    app.MapControllers();
    app.MapFallbackToPage("/_Host");

    app.Run();
}
