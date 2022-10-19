using Microsoft.EntityFrameworkCore;

namespace Demo.Libs.MultiTenancy.EntityFramework.Builders;

public interface IMultitenancyDbModelBuilder
{
    void Configure<TContext>(ModelBuilder builder, string tenant);
}

public interface IMultitenancyDbModelBuilder<TContext>
    where TContext : DbContext
{
    void Configure(ModelBuilder builder, string tenant);
}