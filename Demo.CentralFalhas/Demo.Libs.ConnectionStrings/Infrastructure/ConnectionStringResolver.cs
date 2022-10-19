using Demo.Libs.ConnectionStrings.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Demo.Libs.ConnectionStrings.Infrastructure;

/// <summary>
/// <para>
///     Serviço para resolver as strings de conexão, procurando-as do <see cref="IConfiguration"/>
///     segundo as configurações, tenant e database.
/// </para>
/// </summary>
/// <remarks>
/// <para>
///     Este é um serviço concreto, e o <see cref="ConnectionStringProvider"/> depende dele.
/// </para>
/// <para>
///     Caso, no futuro, seja necessário multiplas implementações, ou alguma substituíção de comportamento,
///     poderá ser criada uma interface para este serviço.
/// </para>
/// </remarks>
public class ConnectionStringResolver
{
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    /// <summary>
    /// Cria nova instância do resolver.
    /// </summary>
    /// <param name="configuration">As configurações do aplicativo.</param>
    /// <param name="loggerFactory">Logger Factory.</param>
    public ConnectionStringResolver(
        IConfiguration configuration, 
        ILoggerFactory loggerFactory)
    {
        _configuration = configuration;
        _logger = loggerFactory.CreateLogger<ConnectionStringResolver>();
    }

    /// <summary>
    /// <para>
    ///     Busca pela string de conexão segundo as configurações, tenant e base de dados.
    /// </para>
    /// </summary>
    /// <param name="options">Opções da conexão.</param>
    /// <param name="tenant">Tenant, optional.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">
    ///     Caso não seja encontrada uma string de conexão para as opções de entrada.
    /// </exception>
    public string Resolve(ConnectionStringOptions options, string? tenant)
    {
        _logger.LogInformation(
            "Searching for connection string for tenant: {Tenant}, and for database: {Database}",
            tenant ?? options.Configurations.DefaultName,
            options.Database);

        var cs = options.Configurations.Format == ConnectionStringConfigurationFormat.KeyValue
            ? ResolveKeyValue(options, tenant)
            : ResolveSection(options, tenant);
        
        if (cs is not null)
            return cs;
            
        throw new ArgumentException(
            string.Format(
                "There is no connection string configured for the tenant: {0}, and for database: {1}",
                tenant,
                options.Database));
    }

    private string? ResolveSection(ConnectionStringOptions options, string? tenant)
    {
        var session = _configuration.GetSection(options.Configurations.ConfigurationKey);
        if (session is null)
            return null;

        if (tenant is not null)
        {
            session = _configuration.GetSection(tenant);
            if (session is null)
                return null;
        }
        else
        {
            var aux = _configuration.GetSection(options.Configurations.DefaultName);
            if (aux is not null)
                session = aux;
        }
        
        return session[options.Database ?? options.Configurations.DefaultName];
    }

    private string? ResolveKeyValue(ConnectionStringOptions options, string? tenant)
    {
        foreach (var key in Keys(options, tenant))
        {
            var cs = _configuration[key];
            if (cs is not null)
                return cs;
        }

        return null;
    }

    private IEnumerable<string> Keys(ConnectionStringOptions options, string? tenant)
    {
        var cfgKey = options.Configurations.ConfigurationKey;
        
        if (tenant is null)
        {
            if (options.Database is null)
            {
                yield return $"{cfgKey}";
                yield return $"{cfgKey}_{options.Configurations.DefaultName}";
                yield return $"{cfgKey}_{options.Configurations.DefaultName}_{options.Configurations.DefaultName}";
            }
            else
            {
                yield return $"{cfgKey}_{options.Database}";
                yield return $"{cfgKey}_{options.Configurations.DefaultName}_{options.Database}";
            }
        }
        else
        {
            if (options.Database is null)
            {
                yield return $"{cfgKey}_{tenant}";
                yield return $"{cfgKey}_{tenant}_{options.Configurations.DefaultName}";
            }
            else
            {
                yield return $"{cfgKey}_{tenant}_{options.Database}";
            }
        }
    }
}