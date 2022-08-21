using TwitterStreamAnalytics.Api;
using TwitterStreamAnalytics.Api.Application;
using TwitterStreamAnalytics.Consumers.Application;
using TwitterStreamAnalytics.SharedKernel.Infrastructure.MessageBus.InMem;

var builder = WebApplication.CreateBuilder(args);

// TODO: replace w/alternate transport for persistence & scale
builder.Services.AddInMemoryMessageBus();

// TODO: extract Consumers to separate process to independently scale out
builder.Services.AddConsumersApplication();

builder.Services.AddApiApplication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.AddRoutes();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();

// HACK: to access from integration tests
// https://stackoverflow.com/a/69483450/2343739
public partial class Program
{
}