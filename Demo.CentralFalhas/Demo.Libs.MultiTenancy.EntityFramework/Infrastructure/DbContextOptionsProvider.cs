using Demo.Libs.ConnectionStrings;
using Demo.Libs.MultiTenancy.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Libs.MultiTenancy.EntityFramework.Infrastructure;

internal class DbContextOptionsProvider : IDbContextOptionsProvider
{
    private readonly ICurrentTenant _tenant;
    private readonly MultiTenancyDbContextConfiguration _configuration;

    public DbContextOptionsProvider(ICurrentTenant tenant, MultiTenancyDbContextConfiguration configuration)
    {
        _tenant = tenant;
        _configuration = configuration;
    }

    public DbContextOptions GetOptions<TContext>()
        where TContext : DbContext
    {
        return _configuration.GetDbContextOption<TContext>(_tenant.Name);
    }
}


public static class Try
{
    // TODO: remover Try no final.
    
    
    public static void Start(IServiceCollection services)
    {
        services.AddDbContext<MyDbContext>((sp, ob) => { });

        services.AddMultiTenancyDbContext<MyDbContext>(ConfigureMyDbContext);
    }

    private static void ConfigureMyDbContext(IServiceProvider sp, string tenant, DbContextOptionsBuilder builder)
    {
        var csProvider = sp.GetRequiredService<IConnectionStringProvider<MyDbContext>>();
        var cs = csProvider.GetConnectionString();
        
    }

    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}