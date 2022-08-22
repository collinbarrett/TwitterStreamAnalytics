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

// to access from integration tests
// https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0#basic-tests-with-the-default-webapplicationfactory
public partial class Program
{
}