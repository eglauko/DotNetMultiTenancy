namespace Demo.Libs.ConnectionStrings;

/// <summary>
/// <para>
///     Atributo para utilizar em classes que requerem strings de conexão,
///     para definir automaticamente o nome da conexão obtida de <see cref="IConnectionStringProvider{TContext}"/>.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ConnectionNameAttribute : Attribute
{
    /// <summary>
    /// Nome da string de conexão.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Define o nome da string de conexão.
    /// </summary>
    /// <param name="name">Nome da string de conexão.</param>
    public ConnectionNameAttribute(string name)
    {
        Name = name;
    }
}