using TwitterStreamAnalytics.SharedKernel.Domain.Aggregates;

namespace TwitterStreamAnalytics.SharedKernel.Domain.Tests.Aggregates;

public class HashtagTests
{
    [Fact]
    public void IncrementCount_OnExistingHashtag_IncrementsCount()
    {
        // Arrange
        var sut = new Hashtag("TestTag");

        // Act
        sut.IncrementCount();

        // Assert
        Assert.True(sut.Count == 2);
    }
}