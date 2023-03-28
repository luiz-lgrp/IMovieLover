using MediatR;

using IMovieLoverAPI.Controllers;
using IMovieLover.API.Behaviors;
using FluentValidation;
using System.Reflection;
using IMovieLover.API.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<ChatGptAuthorizationHandler>();

builder.Services.AddHttpClient<MovieNameCommandHandler>("ChatGpt")
    .AddHttpMessageHandler<ChatGptAuthorizationHandler>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://api.openai.com/v1/"));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var configuration = builder.Configuration;

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
