
// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Logging;

public static class MultiTenancyMiddlewareLoggerExtensions
{
    internal static void LogTenantAssigned(this ILogger logger, string tenantName)
    {
        logger.LogInformation("Current tenant assigned: {TenantName}", tenantName);
    }
    
    internal static void LogTenantNotInformed(this ILogger logger)
    {
        logger.LogInformation("Tenant not informed");
    }
}