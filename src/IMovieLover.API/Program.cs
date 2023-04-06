using System.Reflection;
using System.Net.Http.Headers;
using IMovieLover.API.Handlers;
using IMovieLover.API.Behaviors;

using MediatR;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient<MovieNameCommandHandler>("ChatGpt", client =>
{
    var token = builder.Configuration.GetValue<string>("ChatGptSecretKey");

    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

}).ConfigureHttpClient(client => client.BaseAddress = new Uri("https://api.openai.com/v1/"));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(Program));

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

builder.Services.AddValidatorsFromAssembly(typeof(IMovieLover.API.Behaviors.ValidationPipelineBehavior<,>).Assembly);


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
