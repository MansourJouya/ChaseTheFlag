using ChaseTheFlag.Api.Profile.Games;
using ChaseTheFlag.Api.Profile.Users;
using ChaseTheFlag.Application.Chats;
using ChaseTheFlag.Application.Games;
using ChaseTheFlag.Application.Hubs;
using ChaseTheFlag.Application.Hubs.Serviec;
using ChaseTheFlag.Application.Users;
using ChaseTheFlag.Domain.Entities.Games;
using ChaseTheFlag.Domain.Entities.Users;
using ChaseTheFlag.Domain.Services;
using ChaseTheFlag.Infrastructure.Context;
using ChaseTheFlag.Infrastructure.Middlewares;
using ChaseTheFlag.Infrastructure.Repositories.Chats;
using ChaseTheFlag.Infrastructure.Repositories.Games;
using ChaseTheFlag.Infrastructure.Repositories.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddScoped<IChatPrivateService, ChatPrivateService>();
builder.Services.AddScoped<IChatPrivateRepository, ChatPrivateRepository>();


builder.Services.AddScoped<IChatPublicService, ChatPublicService>();
builder.Services.AddScoped<IChatPublicRepository, ChatPublicRepository>();


builder.Services.AddScoped<IRegisteredUserRepository, RegisteredUserRepository>();
builder.Services.AddScoped<IRegisteredRoomRepository, RegisteredRoomRepository>();
builder.Services.AddScoped<IUserInRoomRepository, UserInRoomRepository>();
builder.Services.AddScoped<IRegisteredUserService, RegisteredUserService>();
builder.Services.AddScoped<IRegisteredRoomService, RegisteredRoomService>();
builder.Services.AddScoped<IUserInRoomService, UserInRoomService>();

builder.Services.AddDbContext<DbContextConnection>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyCustomConnectionString")));

var tokenSecret = "fp7nTBpBqS5UCitNN9ChV5edVCA0DCEZmt8MbPjqOaEDAsinwAFcxDDhkrDeFhFO";
var key = Encoding.ASCII.GetBytes(tokenSecret);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidateActor = true,
            ValidateSignatureLast = true,
            ValidateTokenReplay = true,
            ValidateWithLKG = true,
            RequireExpirationTime = true,
            LogValidationExceptions = true,

        };
    });




builder.Services.AddMemoryCache();
builder.Services.AddScoped<IUserExtractor, UserExtractor>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<HubService>();

builder.Services.AddAutoMapper(
    typeof(ProfileUser),
    typeof(ProfileRoom)
    );


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chase The Flag (Mansour Jouya)", Version = "v1.2" });

    c.SchemaGeneratorOptions.SchemaIdSelector = (type) => type.FullName;

    c.MapType<RegisteredRoom>(() => new OpenApiSchema { /* Define schema properties here */ });
    c.MapType<User>(() => new OpenApiSchema { /* Define schema properties here */ });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();



app.UseCors(builder => builder
    .WithOrigins("https://localhost:44398") // Allow requests from Blazor application origin
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());

app.UseTokenValidation();

app.MapControllers();
app.MapHub<GameHub>("/signalhub");
app.Run();
