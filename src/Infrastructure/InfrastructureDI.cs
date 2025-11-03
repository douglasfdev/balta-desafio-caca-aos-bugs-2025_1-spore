using BugStore.Configuration;
using BugStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BugStore.Infrastructure;

public static class InfrastructureDI
{
    public static void AddInfrastructureDILayer(this IServiceCollection serviceCollection)
        => serviceCollection.AddDbContext<BugStoreContext>(options => options.UseSqlite(EnvironmentVariables.ConnectionStrings?.SqliteStringConnection));
}