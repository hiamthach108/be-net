using Application.Service;
using Application.Services;
using Core.Jwt;
using Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
// using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5000");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddBearerToken();
builder.Services.AddHttpContextAccessor();
// builder.Services.AddDbContext<TodoContext>(opt =>
//     opt.UseInMemoryDatabase("TodoList"));

builder.Services.AddDbContext<TodoDbContext>(options =>
{
    // log the connection string
    Console.WriteLine($"Connection string: {builder.Configuration.GetConnectionString("DatabaseConnection")}");
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection") ?? "");
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{

    options.ClientId = builder.Configuration.GetValue<string>("GoogleClientId") ?? throw new ArgumentNullException("GoogleClientId");
    options.ClientSecret = builder.Configuration.GetValue<string>("GoogleClientSecret") ?? throw new ArgumentNullException("GoogleClientSecret");
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnet template", Version = "v1" });

    // Add a bearer token to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    // Require the bearer token for all API operations
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
      {
          new OpenApiSecurityScheme
          {
              Reference = new OpenApiReference
              {
                  Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
              }
          },
          new string[] {}
      }
    });
});


// Services
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
