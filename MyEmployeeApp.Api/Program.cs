using Microsoft.EntityFrameworkCore;
using MyEmployeeApp.Domain.RepositoryInterfaces;
using MyEmployeeApp.Infrastructure.Configurations;
using MyEmployeeApp.Infrastructure.Data;
using MyEmployeeApp.Infrastructure.Elastic;
using MyEmployeeApp.Infrastructure.Repositories;
using MyEmployeeApp.Service.ServiceInterfaces;
using MyEmployeeApp.Service.Services;
using Nest;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));


var esSettings = builder.Configuration.GetSection("Elasticsearch").Get<ElasticsearchSettings>();

// Register config & client
builder.Services.AddSingleton(esSettings);
builder.Services.AddSingleton<IElasticClient>(ElasticConfig.CreateClient(esSettings));

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeSearchRepository, EmployeeSearchRepository>();

builder.Services.AddScoped<IEmployeeService, EmployeeService>();

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

app.Run();
