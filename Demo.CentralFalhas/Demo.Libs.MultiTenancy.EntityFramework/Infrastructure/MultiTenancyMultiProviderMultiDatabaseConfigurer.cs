using Demo.Libs.ConnectionStrings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Libs.MultiTenancy.EntityFramework.Infrastructure;

public static class MultiTenancyMultiProviderMultiDatabaseConfigurer
{
    public static readonly Action<IServiceProvider, string, DbContextOptionsBuilder> ConfigureDelegate = ConfigureDbContext;
    
    private static void ConfigureDbContext(IServiceProvider sp, string tenant, DbContextOptionsBuilder builder)
    {
        var csProvider = sp.GetRequiredService<IConnectionStringProvider<Try.MyDbContext>>();
        var cs = csProvider.GetConnectionString();
        
        // TODO: continuar implementação.
        
        throw new NotImplementedException();
    }
}
