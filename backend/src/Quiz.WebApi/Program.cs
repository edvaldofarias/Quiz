using Quiz.WebApi.Infrastructures;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStartup(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseStartup(app.Configuration);