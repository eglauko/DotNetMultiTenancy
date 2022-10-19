using Microsoft.EntityFrameworkCore;

namespace Demo.Libs.MultiTenancy.EntityFramework.Builders;

public interface IDbModelBuilder
{
    void Configure<TContext>(ModelBuilder builder);
}

public interface IDbModelBuilder<TContext>
    where TContext : DbContext
{
    void Configure(ModelBuilder builder);
}
