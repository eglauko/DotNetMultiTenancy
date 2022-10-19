using Microsoft.EntityFrameworkCore;

namespace Demo.Libs.MultiTenancy.EntityFramework.Builders;

public interface IMultitenancyDbContextBuilder
{
    void Configure<TContext>(DbContextOptionsBuilder builder, string tenant);
}

public interface IMultitenancyDbContextBuilder<TContext>
    where TContext : DbContext
{
    void Configure(DbContextOptionsBuilder builder, string tenant);
}
