using AspNetCoreLoggingAPI.Data;
using AspNetCoreLoggingAPI.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registering database context with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registering controllers
builder.Services.AddControllers();

// Registering the logging filter to allow [ServiceFilter] usage in controllers
builder.Services.AddScoped<LoggingActionFilter>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
