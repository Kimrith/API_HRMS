using HRMS.API.Commands.Create;
using HRMS.API.Commands.Delete;
using HRMS.API.Commands.Update;
using HRMS.API.Data;
using HRMS.API.Queries;
using HRMS.API.Services; // Ensure this matches your namespace
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Security & Configuration ---
var tokenKey = builder.Configuration["TokenKey"] 
    ?? throw new InvalidOperationException("TokenKey is missing in appsettings.json");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<ITokenService, TokenService>();

// --- 2. Database & API Services ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- 3. Register your CQRS classes ---
builder.Services.AddScoped<UserQueries>();
builder.Services.AddScoped<CreateUserCommand>();
builder.Services.AddScoped<UpdateUserCommand>();
builder.Services.AddScoped<DeleteUserCommand>();

var app = builder.Build();

// --- 4. Pipeline (Must be in this exact order) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapScalarApiReference(options => {
        options.Title = "HRMS API";
        options.Theme = ScalarTheme.Kepler;
        options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
    });
}

app.UseHttpsRedirection();

// ... after app.UseHttpsRedirection();
app.UseStaticFiles(); // <--- ADD THIS LINE
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();