using System.Text.Json.Serialization;
using HRMS.API.Commands.Create;
using HRMS.API.Commands.Delete;
using HRMS.API.Commands.Update;
using HRMS.API.Data;
using HRMS.API.Queries;
using HRMS.API.Services;
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

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // This converts ALL Enums to strings (e.g., "Rejected")
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        
        // Optional: If you want to keep properties camelCase (e.g., "isActive" instead of "IsActive")
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

// builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// --- 3. Register your CQRS classes ---
// Users
builder.Services.AddScoped<UserQueries>();
builder.Services.AddScoped<CreateUserCommand>();
builder.Services.AddScoped<UpdateUserCommand>();
builder.Services.AddScoped<DeleteUserCommand>();

// Employees
builder.Services.AddScoped<EmployeeQueries>();
builder.Services.AddScoped<CreateEmployeeCommand>();
builder.Services.AddScoped<UpdateEmployeeCommand>();
builder.Services.AddScoped<DeleteEmployeeCommand>();

// Attendance
builder.Services.AddScoped<AttendanceLogQueries>();
builder.Services.AddScoped<CreateAttendanceLogCommand>();
builder.Services.AddScoped<UpdateAttendanceLogCommand>();
builder.Services.AddScoped<DeleteAttendanceLogCommand>();

// Payslip
builder.Services.AddScoped<PayslipQueries>();
builder.Services.AddScoped<CreatePayslipCommand>();
builder.Services.AddScoped<UpdatePayslipCommand>();
builder.Services.AddScoped<DeletePayslipCommand>();


// google cloude storage services
// builder.Services.AddSingleton<HRMS.API.Services.GcsService>();

var app = builder.Build();

// --- 4. Pipeline (Middleware) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Scalar is a great modern alternative to Swagger UI
    app.MapScalarApiReference(options => {
        options.Title = "HRMS API";
        options.Theme = ScalarTheme.Kepler;
        options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles(); 

app.UseAuthentication(); // Must come before Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();