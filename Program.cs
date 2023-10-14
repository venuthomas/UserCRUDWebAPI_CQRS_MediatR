using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserCRUDWebAPI_CQRS_MediatR.Context;
using UserCRUDWebAPI_CQRS_MediatR.Features.Commands;
using UserCRUDWebAPI_CQRS_MediatR.Features.Queries;
using UserCRUDWebAPI_CQRS_MediatR.Models;
using UserCRUDWebAPI_CQRS_MediatR.Validations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", false)
               .Build();

var sqlConnection = configuration.GetSection("ConnectionStrings")["SQLConnectionStrings"];
builder.Services.AddDbContext<demoDBContext>(options=> options.UseSqlServer(sqlConnection));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddFluentValidation(options=>
{
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());  
});

//builder.Services.AddTransient(typeof(IPipelineBehavior<UpdateUserDetailsCommand, ResponseDto>), typeof(ValidationHandler));
//builder.Services.AddTransient(typeof(IPipelineBehavior<SaveUserDetailsCommand, ResponseDto>), typeof(ValidationHandler));

var app = builder.Build();

// Configure the HTTP request pipeline.s
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
