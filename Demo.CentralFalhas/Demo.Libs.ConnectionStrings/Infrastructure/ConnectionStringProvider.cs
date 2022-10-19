using System.Collections.Concurrent;
using Demo.Libs.ConnectionStrings.Exceptions;
using Demo.Libs.ConnectionStrings.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Demo.Libs.ConnectionStrings.Infrastructure;

/// <summary>
/// <para>
///     Implementação padrão para <see cref="IConnectionStringProvider"/>.
/// </para>
/// <para>
///     Esta classe foi projetada para trabalhar como singleton per database.
/// </para>
/// <para>
///     Para uma única base de dados, ela pode resolver as strings de conexão por tenant.
/// </para>
/// </summary>
public class ConnectionStringProvider : IConnectionStringProvider
{
    private static readonly ConcurrentDictionary<string, Resolution> Cache = new();
    
    private readonly ConnectionStringOptions _options;
    private readonly ConnectionTenant _tenant;
    private readonly ConnectionStringResolver _resolver;
    private readonly ILogger _logger;

    private readonly Func<string, Resolution> _resolveDelegate;
    
    /// <summary>
    /// Cria uma nova instância do provider.
    /// </summary>
    /// <param name="resolver">Para resolver as strings de conexão.</param>
    /// <param name="options">Opções da conexão.</param>
    /// <param name="tenant">Tenant da conexão.</param>
    /// <param name="logger">Logger.</param>
    public ConnectionStringProvider(
        ConnectionStringResolver resolver,
        ConnectionStringOptions options,
        ConnectionTenant tenant,
        ILogger logger)
    {
        _resolver = resolver;
        _options = options;
        _tenant = tenant;
        _logger = logger;

        _resolveDelegate = Resolve;
    }

    /// <inheritdoc />
    public virtual string GetConnectionString()
    {
        var resolution = Cache.GetOrAdd(_tenant.Name ?? _options.Configurations.DefaultName, _resolveDelegate);
        
        if (resolution.ConnectionString is not null)
            return resolution.ConnectionString;

        throw resolution.Error!;
    }

    private Resolution Resolve(string _)
    {
        try
        {
            var cs = _resolver.Resolve(_options, _tenant.Name);
            return new(cs, null);
        }
        catch (Exception e)
        {
            var error = new ConnectionStringNotFoundException(_options, _tenant.Name, e);
            _logger.LogGetConnectionStringException(error);
            return new(null, error);
        }
    }

    private record Resolution(string? ConnectionString, Exception? Error);
}

/// <inheritdoc cref="IConnectionStringProvider{TContext}"/>
public class ConnectionStringProvider<TContext> : ConnectionStringProvider, IConnectionStringProvider<TContext>
{
    /// <summary>
    /// Creates a new instance of <see cref="ConnectionStringProvider{TContext}"/>.
    /// </summary>
    /// <param name="resolver">Para resolver as strings de conexão.</param>
    /// <param name="options">Opções da conexão.</param>
    /// <param name="tenant">Tenant da conexão.</param>
    /// <param name="loggerFactory">Logger factory.</param>
    public ConnectionStringProvider(
        ConnectionStringResolver resolver,
        OptionsMonitor options,
        ConnectionTenant tenant,
        ILoggerFactory loggerFactory)
    : base(
        resolver, 
        options.Get<TContext>(),
        tenant,
        loggerFactory.CreateLogger<ConnectionStringProvider<TContext>>())
    { }
}

internal static class ConnectionStringProviderLoggerExtensions
{
    public static void LogGetConnectionStringException(this ILogger logger, Exception e)
    {
        logger.LogError(e, "An error occurred when trying to get the connection string");
    }
}