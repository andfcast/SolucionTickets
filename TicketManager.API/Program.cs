using Microsoft.EntityFrameworkCore;
using TicketManager.API.Services.Implementation;
using TicketManager.API.Services.Interfaces;
using TicketManager.Domain.Repositories;
using TicketManager.Infrastructure.Persistence;
using TicketManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

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
