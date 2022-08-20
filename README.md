# TwitterStreamAnalytics

Analyzing the [Twitter 1% Sample Stream](https://developer.twitter.com/en/docs/twitter-api/tweets/sampled-stream/introduction) in .NET

## Get Started

### Configuration

Add a [Twitter App Bearer Token](https://developer.twitter.com/en/docs/authentication/oauth-2-0/bearer-tokens) as an [environment variable](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows#environment-variables):

```
Twitter__AppBearerToken=
```

or [secret](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows#secret-manager):

```
"Twitter:AppBearerToken": ""
```

### Run

1. Start the TwitterStreamAnalytics.Api project in Visual Studio.
2. Use the Swagger UI that appears to interact with the app.

## Architecture

By design, the Consumers application could be extracted to one or more independent instances supporting high-throughput via the competing consumers pattern. The message bus transport could be swapped from in-memory to a persistent/scalable provider such as RabbitMQ. The Entity Framework Core provider could be swapped from in-memory to a persistent/scalable provider such as Cosmos DB. The app seeks to align with DDD and CQRS patterns to support increasing feature complexity.

### Dependency Diagram

```mermaid
flowchart TB;  
	Api-->SharedKernel.Infrastructure.MessageBus.InMem;

	subgraph Message Bus
		SharedKernel.Infrastructure.MessageBus.InMem-->N3([MassTransit]);
	end

	SharedKernel.Infrastructure.MessageBus.InMem-->Api.Application;
	SharedKernel.Infrastructure.MessageBus.InMem-->Consumers.Application;
  
	subgraph Api App
		Api-->N1([Swashbuckle.AspNetCore]);
		Api.Application-->Api.Infrastructure;
		Api.Infrastructure-->N4([TweetinviAPI]);
		Api.Infrastructure-->N5([MassTransit.Abstractions]);  
	end

	subgraph Consumers App
		Consumers.Application-->Consumers.Infrastructure;
		Consumers.Infrastructure-->Consumers.Domain;
		Consumers.Infrastructure-->N6([MassTransit.Abstractions]);
		Consumers.Infrastructure-->N7([Scrutor]);
	end

	Api.Infrastructure-->SharedKernel.Domain;
	Consumers.Domain-->SharedKernel.Domain;
	Api.Infrastructure-->SharedKernel.Infrastructure.Persistence.InMem;
	Consumers.Infrastructure-->SharedKernel.Infrastructure.Persistence.InMem;

	subgraph Persistence
		SharedKernel.Infrastructure.Persistence.InMem-->N8([Microsoft.EntityFrameworkCore.InMemory]);
	end
```