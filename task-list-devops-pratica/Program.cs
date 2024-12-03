using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using task_list_devops_pratica.Context;
using task_list_devops_pratica.Context.Data;
using task_list_devops_pratica.Models.Interfaces;
using task_list_devops_pratica.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "TaskTodo",
        ValidAudience = "TaskTodo",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hardCodedKeyForNow1234567890!_testeeeee"))
    };
});

DotEnv.Load();

var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string not found.");
}

builder.Services.AddDbContext<TaskContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IAppUserData, AppUserData>();
builder.Services.AddScoped<ITaskToDoData, TaskToDoData>();
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
