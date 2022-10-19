using Microsoft.Extensions.Configuration;

namespace Demo.Libs.ConnectionStrings.Options;

/// <summary>
/// Opções para resolver uma string de conexão.
/// </summary>
public class ConnectionStringOptions
{
    /// <summary>
    /// Como estão configuradas as strings de conexão no <see cref="IConfiguration"/>
    /// </summary>
    public ConnectionStringConfigurationOptions Configurations { get; set; } =
        ConnectionStringConfigurationOptions.Default;

    /// <summary>
    /// <para>
    ///     Opcional, o nome da base de dados.
    /// </para>
    /// <para>
    ///     Quando não informado, será utilizado o termo "Default".
    /// </para>
    /// </summary>
    public string? Database { get; set; }
}