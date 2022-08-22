using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TwitterStreamAnalytics.Tests;

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
            TopHashtags = new List<Hashtag>()
        };
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task POST_Start_BeforeStartingStreamReader_StartsReadingStream()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var startResponse = await client.PostAsync(StartUrl, default);

        // Assert
        startResponse.EnsureSuccessStatusCode();
        var statsResponse = await client.GetAsync(StatsUrl);
        statsResponse.EnsureSuccessStatusCode();
        var actual = await statsResponse.Content.ReadFromJsonAsync<Stats>();
        Assert.True(actual?.IsReadingStream);
    }

    [Fact]
    public async Task POST_Stop_AfterStartingStreamReader_StopsReadingStream()
    {
        // Arrange
        var client = _factory.CreateClient();
        var startResponse = await client.PostAsync(StartUrl, default);
        startResponse.EnsureSuccessStatusCode();

        // Act
        var stopResponse = await client.PostAsync(StopUrl, default);

        // Assert
        stopResponse.EnsureSuccessStatusCode();
        var statsResponse = await client.GetAsync(StatsUrl);
        statsResponse.EnsureSuccessStatusCode();
        var actual = await statsResponse.Content.ReadFromJsonAsync<Stats>();
        Assert.False(actual?.IsReadingStream);
    }

    [Fact]
    public async Task GET_Stats_AfterStartingStreamReader_ReturnsNonZeroStats()
    {
        // Arrange
        var client = _factory.CreateClient();
        var startResponse = await client.PostAsync(StartUrl, default);
        startResponse.EnsureSuccessStatusCode();
        Thread.Sleep(10000);

        // Act
        var statsResponse = await client.GetAsync(StatsUrl);

        // Assert
        statsResponse.EnsureSuccessStatusCode();
        var actual = await statsResponse.Content.ReadFromJsonAsync<Stats>();
        Assert.True(actual?.TweetCount > 0);
        Assert.True(actual.HashtagCount > 0);
        Assert.True(actual.TopHashtags.Count > 0);
    }
}