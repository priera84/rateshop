using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UpsAPI;
using UpsAPI.Models.Db;
using UpsAPI.Repositories;
using UpsAPI.Repositories.Interfaces;
using UpsAPI.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RatesDBContext>(options => options.UseInMemoryDatabase("UPSDatabase"));
builder.Services.AddScoped<IRatesRepository, RatesRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

RatesSeedData.EnsurePopulated(app);

app.Run();
