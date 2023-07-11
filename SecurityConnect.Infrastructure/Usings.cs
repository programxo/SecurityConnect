global using System.IdentityModel.Tokens.Jwt; // Token-Creating
global using System.Security.Claims;
global using System.Text;

global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Configuration;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens; // For Hashing

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

global using SecurityConnect.Application.Common.Interfaces.Authentication;
global using SecurityConnect.Application.Common.Interfaces.Services;
global using SecurityConnect.Domain.Entities.Users;
global using SecurityConnect.Application.Common.Interfaces.Persistence;
global using SecurityConnect.Infrastructure.Authentication;
global using SecurityConnect.Infrastructure.Services;
global using SecurityConnect.Infrastructure.Persistence.Repositories;
global using SecurityConnect.Infrastructure.Persistence;




