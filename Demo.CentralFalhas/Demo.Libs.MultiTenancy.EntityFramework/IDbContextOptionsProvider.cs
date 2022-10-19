using Microsoft.EntityFrameworkCore;

namespace Demo.Libs.MultiTenancy.EntityFramework;

public interface IDbContextOptionsProvider
{
    DbContextOptions GetOptions<TContext>() where TContext : DbContext;
}