using System.Runtime.Serialization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public static class DependencyInjection
{
    private static SqliteConnection _conn = new SqliteConnection("DataSource=:memory:");

    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddDbContext<Database>(options =>
        {
	        _conn.Open();
            options.UseSqlite(_conn);
        });

        services.AddTransient<ApplicationService>();

        return services;
    }
}
