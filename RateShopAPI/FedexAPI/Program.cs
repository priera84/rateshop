using FedexAPI.Models.Db;
using FedexAPI.Repositories;
using FedexAPI.Repositories.Interfaces;
using FedexAPI.Utils;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
builder.Services.AddControllers().AddXmlSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RatesDBContext>(options => options.UseInMemoryDatabase("FedexDatabase"));

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
