using Microsoft.EntityFrameworkCore;

namespace Demo.Libs.MultiTenancy.EntityFramework.Builders;

public interface IDbContextBuilder
{
    void Configure<TContext>(DbContextOptionsBuilder builder);
}

public interface IDbContextBuilder<TContext>
    where TContext : DbContext
{
    void Configure(DbContextOptionsBuilder builder);
}
