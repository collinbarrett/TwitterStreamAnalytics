using MassTransit;
using TwitterStreamAnalytics.Api;
using TwitterStreamAnalytics.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.AddRoutes();

//TODO: mv to infrastructure project and/or mv to IHostedService with teardown?
await app.Services.GetRequiredService<IBusControl>().StartAsync();

app.Run();