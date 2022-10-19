using Demo.Libs.MultiTenancy.Abstractions;
using Demo.Libs.MultiTenancy.Abstractions.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Demo.Libs.MultiTenancy.AspNetCore;

public class MultiTenancyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly MultiTenancyOptions _options;
    private readonly ILogger _logger;

    public MultiTenancyMiddleware(
        RequestDelegate next,
        IOptions<MultiTenancyOptions> options,
        ILoggerFactory loggerFactory)
    {
        _next = next;
        _options = options.Value;
        _logger = loggerFactory.CreateLogger<MultiTenancyMiddleware>();
    }
    
    public Task Invoke(HttpContext context, ITenantManager tenantManager)
    {
        if (context.Request.Headers.TryGetValue(_options.Key, out var values))
        {
            string tenantName = values;
            tenantManager.SetCurrentTenant(tenantName);
            _logger.LogTenantAssigned(tenantName);
        }
        else
        {
            _logger.LogTenantNotInformed();
        }

        return _next(context);
    }
}

