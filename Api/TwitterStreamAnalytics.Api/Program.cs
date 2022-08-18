using Microsoft.EntityFrameworkCore;
using TwitterStreamAnalytics.Api;
using TwitterStreamAnalytics.Api.Application;
using TwitterStreamAnalytics.Api.Infrastructure.Persistence.DbContexts;
using TwitterStreamAnalytics.Consumers.Application;
using TwitterStreamAnalytics.Consumers.Infrastructure.Persistence.DbContexts;
using TwitterStreamAnalytics.SharedKernel.Infrastructure.MessageBus.InMem;
using TwitterStreamAnalytics.SharedKernel.Infrastructure.Persistence.InMem;

var builder = WebApplication.CreateBuilder(args);

// TODO: replace w/alternate transport for persistence & scale
builder.Services.AddInMemoryMessageBus();

// TODO: extract Consumers to separate process to independently scale out
builder.Services.AddConsumersApplication();

var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
builder.Services.AddDbContext<CommandDbContext>(o =>
{
    o.UseInMemoryDatabase(nameof(TwitterStreamAnalytics), MyInMemoryDatabase.Root);
    o.UseInternalServiceProvider(serviceProvider);
});
builder.Services.AddDbContext<QueryDbContext>(o =>
{
    o.UseInMemoryDatabase(nameof(TwitterStreamAnalytics), MyInMemoryDatabase.Root);
    o.UseInternalServiceProvider(serviceProvider);
});

builder.Services.AddApiApplication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.ToString()));

var app = builder.Build();
app.AddRoutes();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();