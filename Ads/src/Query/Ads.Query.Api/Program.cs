var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddWebServices();

var app = builder.Build();

app.AddWebApplicationBuilder();

app.Run();