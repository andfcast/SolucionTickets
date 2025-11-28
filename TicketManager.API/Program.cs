using Microsoft.EntityFrameworkCore;
using TicketManager.Application.Middleware;
using TicketManager.Application.Services.Implementation;
using TicketManager.Application.Services.Interfaces;
using TicketManager.Domain.Repositories;
using TicketManager.Infrastructure.Persistence;
using TicketManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Logs to the console
builder.Logging.AddDebug(); // Logs to Visual Studio's Debug output window

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//var connectionString = builder.Configuration.GetConnectionString("TicketDBConnection");
//builder.Services.AddDbContext<TicketDbContext>(options =>
//    options.UseSqlite(connectionString));
builder.Services.AddDbContext<TicketDbContext>();
builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<ITicketRepository, TicketRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IStatusRepository, StatusRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<TicketCustomMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
