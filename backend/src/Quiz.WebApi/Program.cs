using Quiz.WebApi.Infrastructures;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStartup(builder.Configuration);

var app = builder.Build();

app.UseStartup(app.Configuration);