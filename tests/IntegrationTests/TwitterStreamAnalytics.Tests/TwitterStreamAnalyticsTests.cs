using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using TwitterStreamAnalytics.Api.Application.Queries;

namespace TwitterStreamAnalytics.Tests;

// TODO: resolve ChannelClosedException https://github.com/MassTransit/MassTransit/discussions/3283
public class TwitterStreamAnalyticsTests : IClassFixture<WebApplicationFactory<Program>>
{
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
        const string statsUrl = "/stats";
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(statsUrl);

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
}