using Demo.Libs.ConnectionStrings.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Demo.Libs.ConnectionStrings.Options;

/// <summary>
/// Opções de como estão configuradas as strings de conexão no <see cref="IConfiguration"/>
/// </summary>
public class ConnectionStringConfigurationOptions
{
    public static readonly ConnectionStringConfigurationOptions Default = new();
    
    /// <summary>
    /// Formato em que se encontra a configuração das strings de conexão.
    /// </summary>
    public ConnectionStringConfigurationFormat Format { get; set; }

    /// <summary>
    /// Nome base para se obter as strings de conexão das configurações.
    /// </summary>
    public string ConfigurationKey { get; set; } = "ConnectionStrings";

    internal string DefaultName => Format switch
    {
        ConnectionStringConfigurationFormat.Section => Constants.Section.DefaultName,
        ConnectionStringConfigurationFormat.KeyValue => Constants.KeyValue.DefaultName
    };
}