using TwitterStreamAnalytics.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("/start",
    (ITwitterStreamReader streamReader) => streamReader.Start()).WithName("Start");
app.MapPost("/stop",
    (ITwitterStreamReader streamReader) => streamReader.Stop()).WithName("Stop");

app.Run();