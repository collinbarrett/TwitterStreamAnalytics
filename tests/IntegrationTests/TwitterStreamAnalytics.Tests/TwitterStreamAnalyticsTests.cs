using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TwitterStreamAnalytics.Api.Application.Queries;

namespace TwitterStreamAnalytics.Tests;

// TODO: resolve ChannelClosedException on dispose https://github.com/MassTransit/MassTransit/discussions/3283
public class TwitterStreamAnalyticsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private const string StatsUrl = "/stats";
    private const string StartUrl = "/start";
    private const string StopUrl = "/stop";
    private readonly WebApplicationFactory<Program> _factory;

    public TwitterStreamAnalyticsTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _factory = webApplicationFactory;
    }

    [Fact]
    public async Task GET_SwaggerUi_ReturnsSwaggerUi()
    {
        // Arrange
        const string swaggerUiUrl = "/swagger/index.html";
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(swaggerUiUrl);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.Contains("Swagger UI", responseContent);
    }

    [Fact]
    public async Task GET_Stats_BeforeStartingStreamReader_ReturnsInitialStats()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(StatsUrl);

        // Assert
        response.EnsureSuccessStatusCode();
        var actual = await response.Content.ReadFromJsonAsync<Stats>();
        var expected = new Stats
        {
            IsReadingStream = false,
            TweetCount = 0,
            HashtagCount = 0,
            TopHashtags = new List<IHashtag>()
        };
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task POST_Start_BeforeStartingStreamReader_ReturnsIsReadingStream()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PostAsync(StartUrl, default);

        // Assert
        response.EnsureSuccessStatusCode();
        var statsResponse = await client.GetAsync(StatsUrl);
        statsResponse.EnsureSuccessStatusCode();
        var actual = await statsResponse.Content.ReadFromJsonAsync<Stats>();
        Assert.True(actual?.IsReadingStream);
    }
}

internal class Stats : IStats
{
    public bool IsReadingStream { get; set; }
    public int TweetCount { get; set; }
    public int HashtagCount { get; set; }
    public IReadOnlyList<IHashtag> TopHashtags { get; set; }
}