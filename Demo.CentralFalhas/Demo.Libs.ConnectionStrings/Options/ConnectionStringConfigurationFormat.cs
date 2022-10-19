namespace Demo.Libs.ConnectionStrings.Options;

/// <summary>
/// <para>
///     Formato das strings de conexão.
/// </para>
/// <para>
///     As strings de conexão serão obtidas das configurações e este <code>enum</code> define em que formato
///     elas se encontram.
/// </para>
/// <para>
///     Elas podem estar em forma estrurada, como um objeto json, ou apenas na forma de chave valor.
/// </para>
/// <para>
///     Se estiver estruturada, será utilizada as seções das configurações, caso contrário será obtido como um
///     dicionário.
/// </para>
/// </summary>
/// <example>
/// <para>
///     Exemplo no formato <see cref="Section"/>
/// </para>
/// </example>
public enum ConnectionStringConfigurationFormat
{
    /// <summary>
    /// Forma estruturada, onde será obtido a string de conexão pela navegação das sessões.
    /// </summary>
    Section,
    
    /// <summary>
    /// Forma chave valor, onde será obtido a string de conexão diretamente, concatenando as propriedades
    /// das opções;
    /// </summary>
    KeyValue
}