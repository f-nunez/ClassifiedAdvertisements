var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebServices();

var app = builder.Build();

app.AddWebApplicationBuilder();

app.Run();