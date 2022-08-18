using Microsoft.EntityFrameworkCore.Storage;

namespace TwitterStreamAnalytics.SharedKernel.Infrastructure.Persistence.InMem;

public static class MyInMemoryDatabase
{
    public static readonly InMemoryDatabaseRoot Root = new();
}