using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using SecretsSharing.Domain.Entities;
using SecretsSharing.Infrastructure.DataAccess;
using SecretsSharing.Web.Infrastructure.DependencyInjection;
using SecretsSharing.Web.Infrastructure.Middleware;
using SecretsSharing.Web.Infrastructure.Startup;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(new JsonOptionsSetup().Setup);

// Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(new SwaggerGenOptionsSetup().Setup);

// Identity services.
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(new JwtBearerOptionsSetup(
        builder.Configuration["Jwt:SecretKey"] ?? string.Empty,
        builder.Configuration["Jwt:Issuer"] ?? string.Empty).Setup
    );

// Database services.
builder.Services.AddDbContext<AppDbContext>(
    new DbContextOptionsSetup(builder.Configuration.GetConnectionString("AppDatabase")).Setup);
builder.Services.AddAsyncInitializer<DatabaseInitializer>();

// Other dependencies.
ApplicationModule.Register(builder.Services, builder.Configuration);
SystemModule.Register(builder.Services);
MediatRModule.Register(builder.Services);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom middleware.
app.UseMiddleware<ApiExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
