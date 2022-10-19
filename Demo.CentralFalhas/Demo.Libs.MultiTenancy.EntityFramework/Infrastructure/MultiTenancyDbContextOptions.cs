using Microsoft.EntityFrameworkCore;

namespace Demo.Libs.MultiTenancy.EntityFramework.Infrastructure;

internal record MultiTenancyDbContextOptions(
    Type DbContextType,
    Action<IServiceProvider, string, DbContextOptionsBuilder> ConfigureOptions);