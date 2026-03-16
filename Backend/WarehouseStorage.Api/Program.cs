using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WarehouseStorage.Domain.Enums;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.Services;
using WarehouseStorage.Services.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// JWT Token Setup
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        
        ValidateIssuer = true,
        ValidIssuer = "WarehouseStorage",
        ValidateAudience = true,
        ValidAudience = "WarehouseStorage",
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        )
    };
    options.MapInboundClaims = false;
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Debug.WriteLine(context.Exception);
            return Task.CompletedTask;
        }
    };
});

//Build the different policies for access to Endpoints
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireAdministratorRole",
         policy => policy.RequireRole(Role.ADMIN.ToString()))
    .AddPolicy("RequireEmployeeRole",
        policy => policy.RequireRole(Role.EMPLOYEE.ToString()));

builder.Services.AddScoped<AuthService>();

//Setup Database
builder.Services.AddInfrastructure(builder.Configuration);

//Add Identity to have EF manage users
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<WarehouseDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<RoleSeeder>();

var app = builder.Build();

//Ensure roles exist in database. Create if needed

using (var scope = app.Services.CreateScope())
{
    var roleSeeder = scope.ServiceProvider.GetRequiredService<RoleSeeder>();
    await roleSeeder.SeedRolesAsync();
}

app.UseRouting();
if (!app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseHttpsRedirection();
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
