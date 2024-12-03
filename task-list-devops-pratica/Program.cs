using dotenv.net;
using Microsoft.EntityFrameworkCore;
using task_list_devops_pratica.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
