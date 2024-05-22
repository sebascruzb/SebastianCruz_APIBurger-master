﻿using SebastianCruz_APIBurger.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SebastianCruz_APIBurger.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SebastianCruz_APIBurgerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SebastianCruz_APIBurgerContext") ?? throw new InvalidOperationException("Connection string 'SebastianCruz_APIBurgerContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapBurgerEndpoints();

app.MapPromoEndpoints();


app.Run();
